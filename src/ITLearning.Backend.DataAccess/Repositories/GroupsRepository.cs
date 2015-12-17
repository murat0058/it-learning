using Microsoft.Data.Entity;
using Microsoft.Extensions.OptionsModel;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ITLearning.Shared.Extensions;
using ITLearning.Contract.Data.Model.User;
using ITLearning.Backend.Database;
using ITLearning.Backend.Database.Entities;
using ITLearning.Backend.Database.Entities.JunctionTables;
using ITLearning.Contract.Data.Model.Groups;
using ITLearning.Contract.Data.Requests.Groups;
using ITLearning.Contract.Data.Results;
using ITLearning.Contract.Data.Results.Groups;
using ITLearning.Contract.DataAccess.Repositories;
using ITLearning.Contract.Providers;
using ITLearning.Shared.Configs;
using System;
using System.IO;
using ITLearning.Contract.Data.Model.Tasks;

namespace ITLearning.Backend.DataAccess.Repositories
{
    public class GroupsRepository : IGroupsRepository
    {
        private readonly IOptions<DatabaseConfiguration> _dbConfiguration;
        private readonly IAppConfigurationProvider _configurationProvider;

        public GroupsRepository(IOptions<DatabaseConfiguration> dbConfiguration, IAppConfigurationProvider configurationProvider)
        {
            _dbConfiguration = dbConfiguration;
            _configurationProvider = configurationProvider;
        }

        public CommonResult<CreateGroupResult> Create(CreateGroupRequest request)
        {
            using(var context = ContextFactory.GetDbContext(_dbConfiguration))
            {
                var user = context.Users.SingleOrDefault(u => u.UserName == request.UserName);

                if (user != null)
                {
                    var groupWithGivenName = context.Groups.FirstOrDefault(x => x.Name == request.Name);

                    if (groupWithGivenName == null)
                    {
                        var group = new Group
                        {
                            Name = request.Name,
                            Description = request.Description,
                            IsPrivate = request.IsPrivate,
                            Password = request.Password,
                            Owner = user
                        };

                        context.Groups.Add(group);

                        context.UserGroups.Add(new UserGroup
                        {
                            User = user,
                            Group = group
                        });

                        context.SaveChanges();

                        return CommonResult<CreateGroupResult>.Success(new CreateGroupResult { Id = group.Id });
                    }
                    else
                    {
                        return CommonResult<CreateGroupResult>.Failure("Grupa o podanej nazwie już istnieje.");
                    }
                }
                else
                {
                    return CommonResult<CreateGroupResult>.Failure("Nie znaleziono podanego użytkownika.");
                }
            }
        }

        public CommonResult Delete(int groupId)
        {
            using (var context = ContextFactory.GetDbContext(_dbConfiguration))
            {
                var group = context.Groups.SingleOrDefault(x => x.Id == groupId);

                var userGroups = context.UserGroups.Where(x => x.Group.Id == groupId);

                if(userGroups != null && userGroups.Any())
                {
                    foreach (var userGroup in userGroups)
                    {
                        context.Remove(userGroup);
                    }
                }

                if (group != null)
                {
                    context.Remove(group);
                    context.SaveChanges();

                    return CommonResult.Success();
                }
                else
                {
                    return CommonResult.Failure("Podana grupa nie istnieje.");
                }
            }
        }

        public CommonResult<GroupData> Get(int groupId, bool withOwner = false, bool withUsers = false, bool withTasks = false)
        {
            using (var context = ContextFactory.GetDbContext(_dbConfiguration))
            {
                var groups = context.Groups.AsQueryable();

                groups = IncludeAdditionalDataForGroups(withOwner, withUsers, withTasks, groups);

                var group = groups.SingleOrDefault(x => x.Id == groupId);

                if (group != null)
                {
                    var groupData = Mapper.Map<GroupData>(group);

                    GetAdditionalDataForGroup(group, groupData, withOwner, withUsers, withTasks);

                    return CommonResult<GroupData>.Success(groupData);
                }
                else
                {
                    return CommonResult<GroupData>.Failure("Podana grupa nie istnieje.");
                }
            }
        }

        public CommonResult<IEnumerable<GroupData>> GetAll(bool withOwner = false, bool withUsers = false, bool withTasks = false)
        {
            using (var context = ContextFactory.GetDbContext(_dbConfiguration))
            {
                var groups = context.Groups.AsQueryable();

                groups = IncludeAdditionalDataForGroups(withOwner, withUsers, withTasks, groups);

                var groupDataList = new List<GroupData>();

                foreach (var group in groups)
                {
                    var groupData = Mapper.Map<GroupData>(group);

                    GetAdditionalDataForGroup(group, groupData, withOwner, withUsers, withTasks);

                    groupDataList.Add(groupData);
                }

                return CommonResult<IEnumerable<GroupData>>.Success(groupDataList);
            }
        }

        public CommonResult<IEnumerable<GroupData>> GetAllForUser(string userName, bool withOwner = false, bool withUsers = false, bool withTasks = false)
        {
            var getAllResult = GetAll(true, true, true);

            if (getAllResult.IsSuccess)
            {
                var userGroups = getAllResult.Item.Where(x => x.Owner.UserName == userName);

                return CommonResult<IEnumerable<GroupData>>.Success(userGroups);
            }
            else
            {
                return CommonResult<IEnumerable<GroupData>>.Failure(getAllResult.ErrorMessage);
            }
        }

        public CommonResult Update(UpdateGroupRequest request)
        {
            using (var context = ContextFactory.GetDbContext(_dbConfiguration))
            {
                var group = context.Groups
                    .Include(x => x.Owner)
                    .FirstOrDefault(x => x.Id == request.Id);

                if (group == null)
                {
                    return CommonResult.Failure("Podana grupa nie istnieje.");
                }

                group.Name = request.Name.NotNullNorEmpty() ? request.Name : group.Name;
                group.Description = request.Description.NotNullNorEmpty() ? request.Description : group.Description;

                var wasPrivate = group.IsPrivate;

                if(!wasPrivate && request.IsPrivate && request.Password.NotNullNorEmpty())
                {
                    group.IsPrivate = request.IsPrivate;
                    group.Password = request.Password;

                    var userGroups = context.UserGroups.Where(x => x.User.Id != group.Owner.Id).ToList();

                    if (userGroups.Any())
                    {
                        context.Remove(userGroups);
                    }
                }

                if(wasPrivate && !request.IsPrivate)
                {
                    group.IsPrivate = request.IsPrivate;
                    group.Password = string.Empty;
                }

                context.SaveChanges();

                return CommonResult.Success();
            }
        }

        public CommonResult AddUsers(UserGroupRequest request)
        {
            using (var context = ContextFactory.GetDbContext(_dbConfiguration))
            {
                var group = context.Groups.FirstOrDefault(x => x.Id == request.GroupId);

                if (group == null)
                {
                    return CommonResult.Failure("Podana grupa nie istnieje.");
                }

                foreach (var userId in request.Users)
                {
                    var userGroup = context.UserGroups.FirstOrDefault(x => x.Group.Id == request.GroupId && x.User.Id == userId);

                    if (userGroup != null)
                    {
                        return CommonResult.Failure("Podany użytkownik został już dodany.");
                    }

                    context.UserGroups.Add(new UserGroup
                    {
                        Group = new Group { Id = request.GroupId },
                        User = new User { Id = userId }
                    });
                }

                context.SaveChanges();

                return CommonResult.Success();
            }
        }

        public CommonResult RemoveUsers(UserGroupRequest request)
        {
            using (var context = ContextFactory.GetDbContext(_dbConfiguration))
            {
                var group = context.Groups.FirstOrDefault(x => x.Id == request.GroupId);

                if (group == null)
                {
                    return CommonResult.Failure("Podana grupa nie istnieje.");
                }

                foreach (var userId in request.Users)
                {
                    var userGroup = context.UserGroups.FirstOrDefault(x => x.Group.Id == request.GroupId && x.User.Id == userId);

                    if (userGroup == null)
                    {
                        return CommonResult.Failure("Podany użytkownik nie jest członkiem tej grupy.");
                    }

                    context.Remove(userGroup);
                }

                context.SaveChanges();

                return CommonResult.Success();
            }
        }

        private void GetAdditionalDataForGroup(Group group, GroupData groupData, bool withOwner = false, bool withUsers = false, bool withTasks = false)
        {
            using (var context = ContextFactory.GetDbContext(_dbConfiguration))
            {
                if (withOwner)
                {
                    groupData.Owner = Mapper.Map<UserProfileData>(group.Owner);
                }
                if (withUsers)
                {
                    var userIdentifiers = context.UserGroups.Where(x => x.Group.Id == group.Id).Select(x => x.User.Id);

                    var users = new List<UserProfileData>();

                    foreach (var id in userIdentifiers)
                    {
                        var user = context.Users.FirstOrDefault(x => x.Id == id);
                        
                        if(user != null)
                        {
                            var mappedUserData = Mapper.Map<UserProfileData>(user);
                            mappedUserData.ProfileImagePath = GenerateImagePath(user.ImageName);
                            users.Add(mappedUserData);
                        }
                    }

                    groupData.Users = users;
                }
                if (withTasks)
                {
                    groupData.Tasks = group.Tasks != null ? group.Tasks.Select(x => Mapper.Map<TaskListItemData>(x)) : new List<TaskListItemData>();

                    foreach (var task in groupData.Tasks)
                    {
                        task.GroupId = groupData.Id;
                        task.GroupName = groupData.Name;
                    }
                }
            }
        }

        private IQueryable<Group> IncludeAdditionalDataForGroups(bool withOwner, bool withUsers, bool withTasks, IQueryable<Group> groups)
        {
            if (withOwner)
            {
                groups = groups.Include(x => x.Owner);
            }
            if (withUsers)
            {
                groups = groups.Include(x => x.Users);
            }
            if (withTasks)
            {
                groups = groups.Include(x => x.Tasks);
            }

            return groups;
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
