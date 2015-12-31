using ITLearning.Contract.Data.Model.Tasks;
using ITLearning.Contract.Data.Results;
using ITLearning.Contract.Data.Results.Tasks;
using System.Collections.Generic;
using ITLearning.Contract.Enums;
using ITLearning.Contract.Data.Requests.Tasks;
using ITLearning.Contract.Data.Model.Branches;
using ITLearning.Contract.Data.Model.CodeReview;
using ITLearning.Contract.Data.Results.Branches;

namespace ITLearning.Contract.DataAccess.Repositories
{
    public interface ITasksRepository
    {
        TaskBaseData GetBaseData(int id);
        CommonResult<IEnumerable<TaskInstancesForUserResult>> GetForUser(string userName);
        TaskViewTypeEnum GetViewType(int id, string userName);

        List<TaskListItemData> GetList(int? count, bool getUserCompletenessInfo = false, string userName = "");
        List<TaskListItemData> GetList(GetTasksListRequestData request);
        List<TaskInstanceData> GetTaskInstances(int parentTaskId);

        CommonResult<int> Create(CreateTaskRequestData requestData, string userName);
        CommonResult Update(EditTaskRequestData requestData);
        CommonResult<EditBranchesResultData> UpdateBranches(int taskId, IEnumerable<BranchShortData> branches);
        CommonResult<int> Delete(int id);

        CommonResult MarkTaskInstanceAsFinished(int parentTaskId);
        CommonResult<int> CreateTaskInstance(int parentTaskId, string userName);

        CommonResult CreateCodeReview(CodeReviewData requestData);
        CommonResult Activate(int id);

        CommonResult AddGitRepositoryToTask(int id, string repositoryName);

        CommonResult<string> GetRepositoryName(int? taskId = null, int? taskInstanceId = null);
        CommonResult<string> GetTaskOwnerUserName(int id);
        CommonResult AddGitRepositoryToTaskInstance(int id, string repositoryName);
    }
}