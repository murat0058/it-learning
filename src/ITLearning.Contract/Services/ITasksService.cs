using ITLearning.Contract.Data.Model.CodeReview;
using ITLearning.Contract.Data.Model.Tasks;
using ITLearning.Contract.Data.Requests.Groups;
using ITLearning.Contract.Data.Requests.Tasks;
using ITLearning.Contract.Data.Results;
using ITLearning.Contract.Data.Results.Tasks;
using ITLearning.Contract.Enums;
using System.Collections.Generic;

namespace ITLearning.Contract.Services
{
    public interface ITasksService
    {
        CreateTaskRequestData GetDataForCreate();
        EditTaskRequestData GetDataForEdit(int id);
        DeleteTaskRequestData GetDataForDelete(int id);

        TaskViewTypeEnum GetViewType(int id);

        TaskData GetOwnerTaskData(int id);
        TaskData GetInstanceTaskData(int id);
        TaskData GetTaskData(int id);

        CommonResult<List<TaskListItemData>> GetList(int? count, bool getUserCompletenessInfo = false, string userName = "");
        CommonResult<List<TaskListItemData>> GetList(GetTasksListRequestData request);

        IEnumerable<TaskListItemData> GetTaskDataWithCompleteness(GetTasksForGroupRequest request, IEnumerable<TaskListItemData> tasksData);

        CommonResult<CreatedTaskResultData> Create(CreateTaskRequestData requestData);
        CommonResult Activate(int id);
        CommonResult Update(EditTaskRequestData requestData);
        CommonResult Delete(int id);

        CommonResult<int> BeginTask(int id);
        CommonResult FinishTask(int id);
        CommonResult ShowBranch(int taskInstanceId, string branchName);

        CommonResult CreateCodeReview(CodeReviewData requestData);

        CommonResult CreateBranch(int taskId, string branchName, string branchDescription);
        CommonResult DeleteBranch(int taskId, string branchName);
    }
}