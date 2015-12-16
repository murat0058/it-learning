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
using ITLearning.Contract.Providers;
using System.IO;

namespace ITLearning.Backend.Business.Services
{
    public class GroupsService : IGroupsService
    {
        private IGroupsRepository _groupsRepository;
        private IUserRepository _userRepository;
        private IAppConfigurationProvider _configurationProvider;

        public GroupsService(IGroupsRepository groupsRepository, IUserRepository userRepository, IAppConfigurationProvider configurationProvider)
        {
            _groupsRepository = groupsRepository;
            _userRepository = userRepository;
            _configurationProvider = configurationProvider;
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

                if (group.Owner.UserName == request.UserName)
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
                else if (group.Users.FirstOrDefault(x => x.UserName == request.UserName) != null || !group.IsPrivate)
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
            var getGroupsForUserResult = _groupsRepository.GetAllForUser(request.UserName, true, true, true);

            if (getGroupsForUserResult.IsSuccess)
            {
                var groups = getGroupsForUserResult.Item.Take(request.NoOfGroups);

                if (groups.Any())
                {
                    var result = new GetLatestGroupsDataResult
                    {
                        Groups = groups.Select(x => new GroupListItem
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Description = x.Description,
                            IsPrivate = x.IsPrivate,
                            Owner = x.Owner != null ? GetOwnerName(x.Owner) : string.Empty,
                            NoOfTasks = x.Tasks != null ? x.Tasks.Count() : 0,
                            NoOfUsers = x.Users != null ? x.Users.Count() : 0
                        })
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

        public CommonResult<GroupData> GetFullData(GetGroupRequest request)
        {
            var getGroupResult = _groupsRepository.Get(request.GroupId, withOwner: true, withUsers: true, withTasks: true);

            return getGroupResult;
        }

        public CommonResult TryAddUserToPrivateGroup(AddUserToGroupRequest request)
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

        public CommonResult<GetUsersForGroupResult> GetUsersForGroup(GetUsersForGroupRequest request)
        {
            var getUsersResult = _groupsRepository.Get(request.GroupId, true, true);

            if (getUsersResult.IsSuccess)
            {
                var group = getUsersResult.Item;

                if (group.Owner.UserName != request.OwnerName)
                {
                    return CommonResult<GetUsersForGroupResult>.Failure("Jedynie założyciel grupy może widzieć te dane.");
                }

                var users = group.Users.ToList();

                if (users.Any())
                {
                    var usersData = new List<UserData>();

                    foreach (var user in users)
                    {
                        var userData = Mapper.Map<UserData>(user);
                        userData.ProfileImagePath = GenerateImagePath(user.ProfileImagePath);
                        usersData.Add(userData);
                    }

                    return CommonResult<GetUsersForGroupResult>.Success(new GetUsersForGroupResult
                    {
                        Users = usersData
                    });
                }
                else
                {
                    return CommonResult<GetUsersForGroupResult>.Failure("Brak użytkowników.");
                }
            }
            else
            {
                return CommonResult<GetUsersForGroupResult>.Failure(getUsersResult.ErrorMessage);
            }
        }

        public CommonResult<GetUsersForGroupResult> GetUsersForGroupManagement(GetUsersForGroupRequest request)
        {
            var getUsersResult = GetUsersForGroup(request);

            if (getUsersResult.IsSuccess)
            {
                var users = getUsersResult.Item.Users.Where(x => x.UserName != request.OwnerName).ToList();

                if (users.Any())
                {
                    return CommonResult<GetUsersForGroupResult>.Success(new GetUsersForGroupResult
                    {
                        Users = users
                    });
                }
                else
                {
                    return CommonResult<GetUsersForGroupResult>.Failure("Jesteś jedynym użytkownikiem tej grupy.");
                }
            }
            else
            {
                return getUsersResult;
            }
        }

        public CommonResult RemoveUsers(UserGroupRequest request)
        {
            return _groupsRepository.RemoveUsers(request);
        }

        public CommonResult AddUsers(UserGroupRequest request)
        {
            return _groupsRepository.AddUsers(request);
        }

        public CommonResult<GroupListedData> GetList(GetGroupListRequest request)
        {
            var getGroupsResult = _groupsRepository.GetAll(true, true, true);

            if (getGroupsResult.IsSuccess)
            {
                var groups = getGroupsResult.Item;

                if (request.Query.NotNullNorEmpty())
                {
                    groups = groups.Where(x => x.Name.ToLower().Contains(request.Query.ToLower()));
                }

                if(request.AccessType != GroupAccessEnum.All)
                {
                    groups = groups.Where(x => x.IsPrivate == (request.AccessType == GroupAccessEnum.PrivateOnly ? true : false));
                }

                if(request.OwnerType != GroupOwnerTypeEnum.All)
                {
                    groups = groups.Where(x => x.Owner.UserName == request.UserName);
                }

                if (groups.Any())
                {
                    var data = new GroupListedData();

                    var groupsListedData = groups.Select(x => new GroupListItem
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        IsPrivate = x.IsPrivate,
                        Owner = GetOwnerName(x.Owner),
                        NoOfTasks = x.Tasks != null ? x.Tasks.Count() : 0,
                        NoOfUsers = x.Users != null ? x.Users.Count() : 0
                    });

                    data.Groups = groupsListedData;

                    return CommonResult<GroupListedData>.Success(data);
                }
                else
                {
                    return CommonResult<GroupListedData>.Failure("Nie znaleziono grup spełniających podane kryteria.");
                }

            }
            else
            {
                return CommonResult<GroupListedData>.Failure(getGroupsResult.ErrorMessage);
            }
        }

        private string GetOwnerName(UserProfileData user)
        {
            if (string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName))
            {
                return user.UserName;
            }
            else
            {
                return $"{user.FirstName} {user.LastName}";
            }
        }

        private string GenerateImagePath(string imageName)
        {
            if (!string.IsNullOrEmpty(imageName))
            {
                var croppedProfileImage = _configurationProvider.GetProfileCroppedImagesFolderPath() + imageName;
                if (File.Exists(croppedProfileImage))
                {
                    return _configurationProvider.GetProfileCroppedImagesFolderInternalPath() + imageName;
                }
                else
                {
                    return _configurationProvider.GetProfileOriginalImagesFolderInternalPath() + imageName;
                }
            }
            else
            {
                return _configurationProvider.GetProfileDefaultImagePath();
            }
        }
    }
}
