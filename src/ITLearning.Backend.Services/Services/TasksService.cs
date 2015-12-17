using System;
using System.Collections.Generic;
using ITLearning.Contract.Data.Model.Tasks;
using ITLearning.Contract.Data.Requests.Tasks;
using ITLearning.Contract.Data.Results;
using ITLearning.Contract.Services;
using ITLearning.Contract.Enums;
using ITLearning.Contract.Data.Results.Tasks;
using ITLearning.Contract.DataAccess.Repositories;
using System.Linq;

namespace ITLearning.Backend.Business.Services
{
    public class TasksService : ITasksService
    {
        private ITasksRepository _tasksRepository;

        public TasksService(ITasksRepository tasksRepository)
        {
            _tasksRepository = tasksRepository;
        }

        public void Create(CreateTaskRequestData requestData)
        {
            throw new NotImplementedException();
        }

        public CommonResult<List<TaskListItemData>> GetLatest(int count)
        {
            throw new NotImplementedException();
        }

        public CommonResult<IEnumerable<TaskInstancesForUserResult>> GetCompletedTaskInstancesForUser(string userName)
        {
            var getTaskInstancesResult = _tasksRepository.GetForUser(userName);

            if (getTaskInstancesResult.IsSuccess)
            {
                var completed = getTaskInstancesResult.Item.Where(x => x.IsCompleted);

                return CommonResult<IEnumerable<TaskInstancesForUserResult>>.Success(completed);
            }
            else
            {
                return CommonResult<IEnumerable<TaskInstancesForUserResult>>.Failure(getTaskInstancesResult.ErrorMessage);
            }
        }
    }
}