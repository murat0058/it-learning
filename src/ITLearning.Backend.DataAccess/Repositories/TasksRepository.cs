using ITLearning.Backend.Database;
using ITLearning.Contract.Data.Results;
using ITLearning.Contract.Data.Results.Tasks;
using ITLearning.Contract.DataAccess.Repositories;
using ITLearning.Shared.Configs;
using Microsoft.Data.Entity;
using Microsoft.Extensions.OptionsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Backend.DataAccess.Repositories
{
    public class TasksRepository : ITasksRepository
    {
        private readonly IOptions<DatabaseConfiguration> _dbConfiguration;

        public TasksRepository(IOptions<DatabaseConfiguration> dbConfiguration)
        {
            _dbConfiguration = dbConfiguration;
        }

        public CommonResult<IEnumerable<TaskInstancesForUserResult>> GetForUser(string userName)
        {
            using (var context = ContextFactory.GetDbContext(_dbConfiguration))
            {
                var taskInstances = context.TaskInstances
                    .Include(x => x.Task)
                    .Include(x => x.User)
                    .Where(x => x.User.UserName == userName)
                    .ToList();

                var taskInstancesForUser = CommonResult<IEnumerable<TaskInstancesForUserResult>>.Success(taskInstances.Select(x => new TaskInstancesForUserResult
                {
                    UserName = x.User.UserName,
                    TaskId = x.Task.Id,
                    TaskInstanceId = x.Id,
                    IsCompleted = x.IsCompleted,
                    IsDeleted = x.IsDeleted,
                    IsPrivate = x.IsPrivate
                }));

                return taskInstancesForUser;
            }
        }
    }
}
