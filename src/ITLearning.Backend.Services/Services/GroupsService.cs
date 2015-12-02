using AutoMapper;
using ITLearning.Backend.Business.Validators;
using ITLearning.Contract.Data.Requests;
using ITLearning.Contract.Data.Results;
using ITLearning.Contract.Data.Results.Groups;
using ITLearning.Contract.DataAccess.Repositories;
using ITLearning.Contract.Services;
using ITLearning.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITLearning.Contract.Data.Requests.Groups;
using ITLearning.Contract.Enums;
using ITLearning.Contract.Data.Model.Groups;
using ITLearning.Contract.Data.Model.User;

namespace ITLearning.Backend.Business.Services
{
    public class GroupsService : IGroupsService
    {
        private IGroupsRepository _groupsRepository;
        private IUserRepository _userRepository;

        public GroupsService(IGroupsRepository groupsRepository, IUserRepository userRepository)
        {
            _groupsRepository = groupsRepository;
            _userRepository = userRepository;
        }

        public CommonResult<CreateGroupResult> CreateGroup(CreateGroupRequest request)
        {
            var validator = new CreateGroupRequestDataValidator();

            request.Password = request.Password != null ? 
                request.Password.ToBase64() : 
                request.Password;

            request.PasswordConfirmation = request.PasswordConfirmation != null ? 
                request.PasswordConfirmation.ToBase64() : 
                request.Password;

            var validationResult = validator.Validate(request);

            if (validationResult.IsValid)
            {
                var result = _groupsRepository.Create(request);

                if (result.IsSuccess)
                {
                    return CommonResult<CreateGroupResult>.Success(result.Item);
                }
                else
                {
                    return CommonResult<CreateGroupResult>.Failure(result.ErrorMessage);
                }
            }
            else
            {
                return CommonResult<CreateGroupResult>.Failure(validationResult.Errors.First().ErrorMessage);
            }
        }

        public CommonResult DeleteGroup(DeleteGroupRequest request)
        {
            var groupResult = _groupsRepository.Get(request.GroupId, withOwner: true);

            if (groupResult.IsSuccess)
            {
                var group = groupResult.Item;

                if(group.Owner.UserName == request.UserName)
                {
                    return _groupsRepository.Delete(request.GroupId);
                }
                else
                {
                    return CommonResult.Failure("Tylko założyciel grupy może ją usunąć.");
                }
            }
            else
            {
                return CommonResult.Failure(groupResult.ErrorMessage);
            }
        }

        public CommonResult UpdateGroup(UpdateGroupRequest request)
        {
            return _groupsRepository.Update(request);
        }

        public CommonResult<GroupBasicData> GetData(GetGroupRequest request)
        {
            var groupResult = _groupsRepository.Get(request.GroupId);

            if (groupResult.IsSuccess)
            {
                return CommonResult<GroupBasicData>.Success(Mapper.Map<GroupBasicData>(groupResult.Item));
            }
            else
            {
                return CommonResult<GroupBasicData>.Failure(groupResult.ErrorMessage);
            }
        }

        public CommonResult<GroupAccessTypeResult> GetAccessType(GroupAccessTypeRequest request)
        {
            var groupResult = _groupsRepository.Get(request.GroupId, withOwner: true, withUsers: true);

            if (groupResult.IsSuccess)
            {
                var group = groupResult.Item;

                var accessType = GroupAccessTypeEnum.RequirePassword;

                if (group.Owner.UserName == request.UserName)
                {
                    accessType = GroupAccessTypeEnum.Owner;
                }
                else if(group.Users.FirstOrDefault(x => x.UserName == request.UserName) != null || !group.IsPrivate)
                {
                    accessType = GroupAccessTypeEnum.Standard;
                }

                return CommonResult<GroupAccessTypeResult>.Success(new GroupAccessTypeResult { GroupAccessTypeEnum = accessType });

            }
            else
            {
                return CommonResult<GroupAccessTypeResult>.Failure(groupResult.ErrorMessage);
            }
        }

        public CommonResult<GetLatestGroupsDataResult> GetLatestGroupsData(GetLatestGroupsBasicDataRequest request)
        {
            var getGroupsForUserResult = _groupsRepository.GetAllForUser(request.UserName, false, true, false);

            if (getGroupsForUserResult.IsSuccess)
            {
                var groups = getGroupsForUserResult.Item.Take(request.NoOfGroups);

                if (groups.Any())
                {
                    var result = new GetLatestGroupsDataResult
                    {
                        Groups = groups.Select(x => Mapper.Map<GroupWithUsersData>(x))
                    };

                    return CommonResult<GetLatestGroupsDataResult>.Success(result);
                }
                else
                {
                    return CommonResult<GetLatestGroupsDataResult>.Failure("Nie jesteś członkiem żadnej grupy.");
                }
            }
            else
            {
                return CommonResult<GetLatestGroupsDataResult>.Failure(getGroupsForUserResult.ErrorMessage);
            }
        }

        public CommonResult<GroupWithUsersData> GetDataWithUsers(GetGroupRequest request)
        {
            var getGroupResult = _groupsRepository.Get(request.GroupId, withOwner: true, withUsers: true);

            if (getGroupResult.IsSuccess)
            {
                var group = getGroupResult.Item;

                GroupWithUsersData data = new GroupWithUsersData
                {
                    Id = group.Id,
                    Name = group.Name,
                    Description = group.Description,
                    IsPrivate = group.IsPrivate,
                    Password = group.Password,
                    Users = group.Users.Select(x => Mapper.Map<UserProfileData>(x)),
                };

                return CommonResult<GroupWithUsersData>.Success(data);
            }
            else
            {
                return CommonResult<GroupWithUsersData>.Failure(getGroupResult.ErrorMessage);
            }
        }

        public CommonResult TryAddUserToGroup(AddUserToGroupRequest request)
        {
            var getUserProfileDataResult = _userRepository.GetUserProfile(request.UserName);
            var getGroup = GetData(new GetGroupRequest { GroupId = request.GroupId });

            if (getUserProfileDataResult.IsSuccess && getGroup.IsSuccess)
            {
                if (getGroup.Item.IsPrivate)
                {
                    var isPasswordCorrect = getGroup.Item.Password == request.Password.ToBase64();

                    if (!isPasswordCorrect)
                    {
                        return CommonResult.Failure("Błędne hasło.");
                    }
                }

                var userGroupRequest = new UserGroupRequest
                {
                    GroupId = request.GroupId,
                    Users = new List<int> { getUserProfileDataResult.Item.Id }
                };

                var addUserResult = _groupsRepository.AddUsers(userGroupRequest);

                if (addUserResult.IsSuccess)
                {
                    return CommonResult.Success();
                }
                else
                {
                    return CommonResult.Failure(addUserResult.ErrorMessage);
                }

            }
            else
            {
                return CommonResult.Failure("Błąd pobierania danych.");
            }
        }
    }
}
