using System.Collections.Generic;
using ITLearning.Contract.Data.Model.Tasks;
using ITLearning.Contract.Data.Requests.Tasks;
using ITLearning.Contract.Data.Results;
using ITLearning.Contract.Services;
using ITLearning.Contract.Enums;
using ITLearning.Contract.Data.Results.Tasks;
using ITLearning.Contract.DataAccess.Repositories;
using System.Linq;
using AutoMapper;
using ITLearning.Contract.Data.Model.Groups;
using ITLearning.Contract.Data.Requests.Groups;
using ITLearning.Shared;
using ITLearning.Shared.Extensions;
using ITLearning.Contract.Data.Model.CodeReview;
using System;
using ITLearning.Contract.Data.Requests.Repositories;
using System.Net;
using Newtonsoft.Json;
using System.Text;
using ITLearning.Shared.Formatters;

namespace ITLearning.Backend.Business.Services
{
    public class TasksService : ITasksService
    {
        private readonly ITasksRepository _tasksRepository;
        private readonly IGroupsRepository _groupsRepository;
        private readonly IUserService _userService;

        private readonly string _sourceControlUrl;
        private readonly string _authKey;

        public TasksService(ITasksRepository tasksRepository, IGroupsRepository groupsRepository, IUserService userService)
        {
            _tasksRepository = tasksRepository;
            _groupsRepository = groupsRepository;
            _userService = userService;

            _sourceControlUrl = "http://localhost:2214/";
            _authKey = "D756F8A9-06E4-43A6-A19E-CFAA0E69ABDB";
        }

        public CreateTaskRequestData GetDataForCreate()
        {
            var data = new CreateTaskRequestData();
            data.UserGroups = new List<UserGroupData>
            {
                new UserGroupData() { Id = -1, Name = "Brak" }
            };
            data.UserGroups.AddRange(Mapper.Map<List<UserGroupData>>(_groupsRepository.GetAllForUser(StaticManager.UserName, true).Item));
            data.AvailableLanguages = default(LanguageEnum).GenerateDisplayDataList();

            return data;
        }

        public EditTaskRequestData GetDataForEdit(int id)
        {
            var taskBaseData = _tasksRepository.GetBaseData(id);

            return Mapper.Map<EditTaskRequestData>(taskBaseData);
        }

        public DeleteTaskRequestData GetDataForDelete(int id)
        {
            var data = new DeleteTaskRequestData()
            {
                Id = id
            };

            return data;
        }

        public TaskViewTypeEnum GetViewType(int id)
        {
            return _tasksRepository.GetViewType(id, StaticManager.UserName);
        }

        public CommonResult<List<TaskListItemData>> GetList(int? count, bool getUserCompletenessInfo = false, string userName = "")
        {
            var result = _tasksRepository.GetList(count, getUserCompletenessInfo, userName);

            if (!result.Any())
            {
                return CommonResult<List<TaskListItemData>>.Failure("Brak aktualnych zadań.");
            }

            return CommonResult<List<TaskListItemData>>.Success(result);
        }

        public CommonResult<List<TaskListItemData>> GetList(GetTasksListRequestData request)
        {
            var result = _tasksRepository.GetList(request);

            if (!result.Any())
            {
                return CommonResult<List<TaskListItemData>>.Failure("Nie znaleziono zadań spełniających podane kryteria.");
            }

            return CommonResult<List<TaskListItemData>>.Success(result);
        }

        public TaskData GetOwnerTaskData(int id)
        {
            var data = new TaskData();
            data = Mapper.Map<TaskData>(_tasksRepository.GetBaseData(id));
            data.TaskInstances = _tasksRepository.GetTaskInstances(id);

            return data;
        }

        public TaskData GetInstanceTaskData(int id)
        {
            var data = new TaskData();
            data = Mapper.Map<TaskData>(_tasksRepository.GetBaseData(id));
            data.TaskInstances = _tasksRepository.GetTaskInstances(id);

            return data;
        }

        public TaskData GetTaskData(int id)
        {
            var data = Mapper.Map<TaskData>(_tasksRepository.GetBaseData(id));

            return data;
        }

        public CommonResult<CreatedTaskResultData> Create(CreateTaskRequestData requestData)
        {
            var result = _tasksRepository.Create(requestData, StaticManager.UserName);

            if (result.IsSuccess)
            {
                var repositoryName = GenerateRepositoryName(StaticManager.UserName);
                var createRepositoryRequestData = new CreateRepositoryRequestData()
                {
                    OwnerName = StaticManager.UserName,
                    RepositoryName = repositoryName,
                    IsPublic = false,
                    IsAnonymousPushAllowed = false
                };

                var userPasswordHash = _userService.GetUserPassword(StaticManager.UserName).Item;
                var encodedDataAsBytes = Convert.FromBase64String(userPasswordHash);
                var userPassword = Encoding.ASCII.GetString(encodedDataAsBytes);
                var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(StaticManager.UserName + ":" + userPassword));
                var credentialsHeader = string.Format("Basic {0}", credentials);

                WebClient webClient = new WebClient();
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                webClient.Headers[HttpRequestHeader.Authorization] = credentialsHeader;
                webClient.Headers["AuthKey"] = _authKey;
                webClient.UploadString(_sourceControlUrl + "Repository/Create", JsonConvert.SerializeObject(createRepositoryRequestData));

                _tasksRepository.AddGitRepositoryToTask(result.Item, repositoryName);

                var data = new CreatedTaskResultData();
                data.Id = result.Item;
                data.RepositoryLink = UrlFormatter.FormatSourceControlUrl(repositoryName);
                data.ShouldActivateTask = requestData.IsActive;

                return CommonResult<CreatedTaskResultData>.Success(data);
            }

            return CommonResult<CreatedTaskResultData>.Failure(result.ErrorMessage);
        }

        public CommonResult Activate(int id)
        {
            return _tasksRepository.Activate(id);
        }

        public CommonResult Update(EditTaskRequestData requestData)
        {
            var result = _tasksRepository.Update(requestData);

            var updatedBranchesResult = _tasksRepository.UpdateBranches(requestData.Id, requestData.Branches);

            foreach (var branch in updatedBranchesResult.Item.BranchesToAdd)
            {
                var branchEditData = requestData.Branches.First(x => x.Name == branch);
                CreateBranch(requestData.Id, branchEditData.Name, branchEditData.Description);
            }

            foreach (var branch in updatedBranchesResult.Item.BranchesToDelete)
            {
                var branchEditData = requestData.Branches.First(x => x.Name == branch);
                DeleteBranch(requestData.Id, branchEditData.Name);
            }

            return result;
        }

        public CommonResult Delete(int id)
        {
            var result = _tasksRepository.Delete(id);

            if (!result.IsSuccess)
            {
                return CommonResult.Failure(result.ErrorMessage);
            }

            //todo AB delete repo from source control
            //result.Item -> IdRepository to Delete

            return CommonResult.Success();
        }

        public CommonResult FinishTask(int id)
        {
            var result = _tasksRepository.MarkTaskInstanceAsFinished(id);

            return result;
        }

        public CommonResult CreateCodeReview(CodeReviewData requestData)
        {
            return _tasksRepository.CreateCodeReview(requestData);
        }

        public CommonResult<int> BeginTask(int id)
        {
            var result = _tasksRepository.CreateTaskInstance(id, StaticManager.UserName);

            //TODO AB Create Task Instance Repository from source control

            return result;
        }

        public CommonResult ShowBranch(int taskInstanceId, string branchName)
        {
            //TODO AB Show Branch from source control

            return CommonResult.Success();
        }

        public CommonResult CreateBranch(int taskId, string branchName, string branchDescription)
        {
            //TODO AB CreateBranch

            return CommonResult.Success();
        }

        public CommonResult DeleteBranch(int taskId, string branchName)
        {
            //TODO AB DeleteBranch

            return CommonResult.Success();
        }

        public IEnumerable<TaskListItemData> GetTaskDataWithCompleteness(GetTasksForGroupRequest request, IEnumerable<TaskListItemData> tasksData)
        {
            List<TaskListItemData> data = new List<TaskListItemData>();

            var getTaskInstances = GetCompletedTaskInstancesForUser(request.UserName);

            if (getTaskInstances.IsSuccess)
            {
                var completedTasks = getTaskInstances.Item.Select(x => x.TaskId).Distinct();

                foreach (var task in tasksData)
                {
                    if (completedTasks.Contains(task.Id))
                    {
                        task.IsCompleted = true;
                    }
                    data.Add(task);
                }
            }

            return data;
        }

        private CommonResult<IEnumerable<TaskInstancesForUserResult>> GetCompletedTaskInstancesForUser(string userName)
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

        private string GenerateRepositoryName(string userName)
        {
            return userName + "-" + Guid.NewGuid().ToString();
        }
    }
}