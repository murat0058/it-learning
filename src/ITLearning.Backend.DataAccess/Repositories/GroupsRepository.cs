using ITLearning.Backend.Database;
using ITLearning.Backend.Database.Entities;
using ITLearning.Backend.Database.Entities.JunctionTables;
using ITLearning.Contract.Data.Requests;
using ITLearning.Contract.Data.Results;
using ITLearning.Contract.Data.Results.Groups;
using ITLearning.Contract.DataAccess.Repositories;
using ITLearning.Contract.Providers;
using ITLearning.Shared.Configs;
using Microsoft.Extensions.OptionsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITLearning.Contract.Data.Model.Groups;
using Microsoft.Data.Entity;

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

        public CommonResult<CreateGroupResult> CreateGroup(CreateGroupRequestData requestData)
        {
            using (var context = ContextFactory.GetDbContext(_dbConfiguration))
            {
                var user = context.Users.SingleOrDefault(u => u.UserName == requestData.UserName);

                if(user != null)
                {
                    var groupWithGivenName = context.Groups.FirstOrDefault(x => x.Name == requestData.Name);

                    if (groupWithGivenName == null)
                    {
                        var group = new Group
                        {
                            Name = requestData.Name,
                            Description = requestData.Description,
                            IsPrivate = requestData.IsPrivate,
                            Password = requestData.Password,
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

        public CommonResult<IEnumerable<GroupBasicData>> GetGroupsBasicData()
        {
            using (var context = ContextFactory.GetDbContext(_dbConfiguration))
            {
                var groups = context.Groups
                    .Include(g => g.Owner)
                    .Include(g => g.Users)
                    .Include(g => g.Tasks)
                    .ToList();

                var result = groups.Select(x => new GroupBasicData
                 {
                     Id = x.Id,
                     Name = x.Name,
                     IsPrivate = x.IsPrivate,
                     Description = x.Description,
                     OwnerId = x.Owner.Id,
                     OwnerUserName = x.Owner.UserName,
                     OwnerFullName = $"{x.Owner.FirstName} {x.Owner.LastName}",
                     NoOfUsers = x.Users.Count(),
                     NoOfTasks = x.Tasks.Count()
                 });

                if (result.Any())
                {
                    return CommonResult<IEnumerable<GroupBasicData>>.Success(result);
                }
                else
                {
                    return CommonResult<IEnumerable<GroupBasicData>>.Failure("Aktualnie nie ma żadnych grup.");
                }
            }
        }
    }
}
