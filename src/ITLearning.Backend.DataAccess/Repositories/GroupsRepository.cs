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
using Microsoft.Data.Entity;
using Microsoft.Extensions.OptionsModel;
using System.Collections.Generic;
using System.Linq;
using System;
using AutoMapper;
using ITLearning.Shared.Extensions;
using ITLearning.Contract.Data.Model.User;

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

        public CommonResult Update(UpdateGroupRequest request)
        {
            using (var context = ContextFactory.GetDbContext(_dbConfiguration))
            {
                var group = context.Groups.FirstOrDefault(x => x.Id == request.Id);

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

                    var userGroups = context.UserGroups.Where(x => x.User.Id != group.Owner.Id);

                    context.Remove(userGroups);
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

                    context.UserGroups.Add(Mapper.Map<UserGroup>(request));
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
                    var users = group.Users
                        .Select(userGroup => context.Users.FirstOrDefault(u => u.Id == userGroup.User.Id))
                        .Select(user => Mapper.Map<UserProfileData>(user))
                        .ToList();

                    groupData.Users = users;
                }
            }
        }
    }
}
