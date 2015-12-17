using System;
using System.Collections.Generic;
using ITLearning.Contract.Data.Model.Tasks;
using ITLearning.Contract.Data.Requests.Tasks;
using ITLearning.Contract.Data.Results;
using ITLearning.Contract.Services;
using ITLearning.Contract.Enums;

namespace ITLearning.Backend.Business.Services
{
    public class TasksService : ITasksService
    {
        public void Create(CreateTaskRequestData requestData)
        {
            throw new NotImplementedException();
        }

        public CommonResult<List<TaskListItemData>> GetLatest(int count)
        {
            throw new NotImplementedException();
        }
    }
}