using ITLearning.Contract.Data.Model.Tasks;
using ITLearning.Contract.Data.Requests.Tasks;
using ITLearning.Contract.Data.Results;
using System.Collections.Generic;

namespace ITLearning.Contract.Services
{
    public interface ITasksService
    {
        void Create(CreateTaskRequestData requestData);
        CommonResult<List<TaskListItemData>> GetLatest(int count);
        CommonResult<List<TaskListItemData>> GetForGroup(int groupId);
    }
}