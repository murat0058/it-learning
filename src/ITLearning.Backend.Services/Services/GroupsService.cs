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

namespace ITLearning.Backend.Business.Services
{
    public class GroupsService : IGroupsService
    {
        private IGroupsRepository _groupsRepository;

        public GroupsService(IGroupsRepository groupsRepository)
        {
            _groupsRepository = groupsRepository;
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

        public CommonResult<GroupBasicData> GetBasicData(GroupBasicDataRequest request)
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

        public CommonResult<GetLatestGroupsBasicDataResult> GetLatestGroupsBasicData(GetLatestGroupsBasicDataRequest request)
        {
            //var groupsResult = _groupsRepository.GetAll(false, true, false);

            if (true)
            {
                return CommonResult<GetLatestGroupsBasicDataResult>.Success(new GetLatestGroupsBasicDataResult
                {
                    Groups = new List<GroupWithUsersBasicData>
                    {
                        new GroupWithUsersBasicData { Id = 16, Name = "Test 1", Description = "Aaaaaaa", IsPrivate = true, NoOfUsers = 10 },
                        new GroupWithUsersBasicData { Id = 17, Name = "Test 2", Description = "Aaaaaaa", IsPrivate = false, NoOfUsers = 5 },
                        new GroupWithUsersBasicData { Id = 18, Name = "Test 3", Description = "Aaaaaaa", IsPrivate = false, NoOfUsers = 17 }
                    }
                });
            }
            else
            {
                return CommonResult<GetLatestGroupsBasicDataResult>.Failure("Test message");
            }
        }
    }
}
