using ITLearning.Backend.Database;
using ITLearning.Contract.Data.Model.Tasks;
using ITLearning.Contract.Data.Results;
using ITLearning.Contract.Data.Results.Tasks;
using ITLearning.Contract.DataAccess.Repositories;
using ITLearning.Shared.Configs;
using Microsoft.Data.Entity;
using Microsoft.Extensions.OptionsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using ITLearning.Contract.Enums;
using AutoMapper;
using ITLearning.Contract.Data.Requests.Tasks;
using ITLearning.Backend.Database.Entities;
using ITLearning.Contract.Data.Model.Branches;
using ITLearning.Contract.Data.Model.CodeReview;
using ITLearning.Shared;
using ITLearning.Shared.Extensions;

namespace ITLearning.Backend.DataAccess.Repositories
{
    public class TasksRepository : ITasksRepository
    {
        private readonly IOptions<DatabaseConfiguration> _dbConfiguration;

        public TasksRepository(IOptions<DatabaseConfiguration> dbConfiguration)
        {
            _dbConfiguration = dbConfiguration;
        }

        public TaskBaseData GetBaseData(int id)
        {
            using (var context = ContextFactory.GetDbContext(_dbConfiguration))
            {
                var data = context.Tasks.Include(x => x.Group)
                                        .Include(x => x.TaskInstances)
                                        .Include(x => x.User)
                                        .Include(x => x.GitRepository)
                                        .First(x => x.Id == id);

                return Mapper.Map<TaskBaseData>(data);
            }
        }

        public CommonResult<IEnumerable<TaskInstancesForUserResult>> GetForUser(string userName)
        {
            using (var context = ContextFactory.GetDbContext(_dbConfiguration))
            {
                //TODO delete .ToList() when after EF RC2 release
                var taskInstances = context.TaskInstances
                    .Include(x => x.Task)
                    .Include(x => x.User)
                    .ToList()
                    .Where(x => x.Task.IsDeleted == false && x.Task.IsActive == true)
                    .Where(x => x.User.UserName == userName);

                var taskInstancesForUser = CommonResult<IEnumerable<TaskInstancesForUserResult>>.Success(taskInstances.Select(x => new TaskInstancesForUserResult
                {
                    UserName = x.User.UserName,
                    TaskId = x.Task.Id,
                    TaskInstanceId = x.Id,
                    IsCompleted = x.IsFinished
                }));

                return taskInstancesForUser;
            }
        }

        public List<TaskListItemData> GetList(int? count, bool getUserCompletenessInfo = false, string userName = "")
        {
            using (var context = ContextFactory.GetDbContext(_dbConfiguration))
            {
                var data = context.Tasks.Include(x => x.Group)
                                        .Include(x => x.User)
                                        .Where(x => x.IsDeleted == false && x.IsActive == true);

                if (!string.IsNullOrEmpty(userName))
                {
                    data = data.Where(x => x.User.UserName == userName);
                }

                data = data.OrderByDescending(x => x.DateOfCreation);

                if (count.HasValue)
                {
                    data = data.Take(count.Value);
                }

                var result = Mapper.Map<List<TaskListItemData>>(data.ToList());

                if (getUserCompletenessInfo)
                {
                    foreach (var item in result)
                    {
                        item.IsCompleted = context.TaskInstances.FirstOrDefault(x => x.Task.Id == item.Id
                                                                        && x.User.UserName == StaticManager.UserName && x.IsFinished) != null ? true : false;
                    }
                }

                return result;
            }
        }

        public List<TaskListItemData> GetList(GetTasksListRequestData request)
        {
            using (var context = ContextFactory.GetDbContext(_dbConfiguration))
            {
                var tasks = context.Tasks.Include(x => x.Group)
                                        .Include(x => x.User)
                                        .Where(x => x.IsDeleted == false);

                if (request.Query.NotNullNorEmpty())
                {
                    tasks = tasks.Where(x => x.Name.ToLower().Contains(request.Query.ToLower()));
                }

                if (request.OwnerType == TaskOwnerTypeEnum.OnlyMine)
                {
                    tasks = tasks.Where(x => x.User.UserName == StaticManager.UserName);
                }

                if (request.Language.ToString() != LanguageFilterEnum.All.ToString())
                {
                    tasks = tasks.Where(x => x.Language.ToString() == request.Language.ToString());
                }

                if (request.ActivityStatus == TaskActivityStatusEnum.Active)
                {
                    tasks = tasks.Where(x => x.IsActive == true);
                }

                if (request.ActivityStatus == TaskActivityStatusEnum.NotActive)
                {
                    tasks = tasks.Where(x => x.IsActive == false);
                }

                var result = Mapper.Map<List<TaskListItemData>>(tasks.ToList());

                return result;
            }
        }

        public List<TaskInstanceData> GetTaskInstances(int parentTaskId)
        {
            using (var context = ContextFactory.GetDbContext(_dbConfiguration))
            {
                //TODO delete .ToList() when after EF RC2 release
                var data = context.TaskInstances
                                  .Include(x => x.GitRepository)
                                  .Include(x => x.Task)
                                  .Include(x => x.TaskInstanceReview)
                                  .Include(x => x.User)
                                  .ToList()
                                  .Where(x => x.Task.IsDeleted == false && x.Task.IsActive == true)
                                  .Where(x => x.Task.Id == parentTaskId);

                var result = Mapper.Map<List<TaskInstanceData>>(data);

                foreach (var item in result)
                {
                    if (!string.IsNullOrEmpty(item.FinishDate) && item.CodeReview != null)
                    {
                        var finishDate = Convert.ToDateTime(item.FinishDate);
                        var stardDate = Convert.ToDateTime(item.StartDate);

                        item.CodeReview.NumberOfActivityDays = finishDate.Subtract(stardDate).Days;
                    }
                }

                return result;
            }
        }

        public TaskViewTypeEnum GetViewType(int id, string userName)
        {
            using (var context = ContextFactory.GetDbContext(_dbConfiguration))
            {
                var taskExist = context.Tasks.Any(x => x.User.UserName == userName && x.Id == id);

                if (taskExist)
                {
                    return TaskViewTypeEnum.OwnerView;
                }

                var taskInstanceExist = context.TaskInstances.Any(x => x.User.UserName == userName && x.Task.Id == id);

                if (taskInstanceExist)
                {
                    return TaskViewTypeEnum.InstanceView;
                }

                return TaskViewTypeEnum.PublicView;
            }
        }

        public CommonResult<int> Create(CreateTaskRequestData requestData, string userName)
        {
            using (var context = ContextFactory.GetDbContext(_dbConfiguration))
            {
                var data = new Task();
                data.DateOfCreation = DateTime.Now;
                data.Description = requestData.Description;
                data.IsActive = requestData.IsActive;
                data.Language = requestData.SelectedLanguage;
                data.Name = requestData.Name;
                data.User = context.Users.First(x => x.UserName == userName);

                if (requestData.SelectedGroupId != -1)
                {
                    data.Group = context.Groups.First(x => x.Id == requestData.SelectedGroupId);
                }

                context.Tasks.Add(data);
                context.SaveChanges();

                return CommonResult<int>.Success(data.Id);
            }
        }

        public CommonResult Activate(int id)
        {
            using (var context = ContextFactory.GetDbContext(_dbConfiguration))
            {
                var data = context.Tasks.First(x => x.Id == id);
                data.IsActive = true;

                context.SaveChanges();

                return CommonResult.Success();
            }
        }

        public CommonResult Update(EditTaskRequestData requestData)
        {
            using (var context = ContextFactory.GetDbContext(_dbConfiguration))
            {
                var data = context.Tasks.First(x => x.Id == requestData.Id);
                data.Description = requestData.Description;
                data.IsActive = requestData.IsActive;
                data.Language = requestData.EditedLanguage;
                data.Name = requestData.Name;

                if (requestData.SelectedGroup != null)
                {
                    data.Group = context.Groups.First(x => x.Id == requestData.SelectedGroup.Id);
                }

                context.SaveChanges();
            }

            return CommonResult.Success();
        }

        public CommonResult<IEnumerable<string>> UpdateBranches(int taskId, IEnumerable<BranchShortData> branches)
        {
            var branchesToDelete = new List<string>();

            using (var context = ContextFactory.GetDbContext(_dbConfiguration))
            {
                var data = context.Tasks.First(x => x.Id == taskId);

                foreach (var branch in data.GitRepository.Branches)
                {
                    var editedBranch = branches.FirstOrDefault(x => x.Name == branch.DisplayName);

                    if (editedBranch == null)
                    {
                        branchesToDelete.Add(branch.Name);
                    }

                    branch.DisplayName = editedBranch.Name;
                    branch.Description = editedBranch.Description;
                }

                context.SaveChanges();

                return CommonResult<IEnumerable<string>>.Success(branchesToDelete);
            }
        }

        public CommonResult<int> Delete(int id)
        {
            using (var context = ContextFactory.GetDbContext(_dbConfiguration))
            {
                var data = context.Tasks.First(x => x.Id == id);
                data.IsDeleted = true;

                context.SaveChanges();

                return CommonResult<int>.Success(data.GitRepository.Id);
            }
        }

        public CommonResult<int> CreateTaskInstance(int parentTaskId, string userName)
        {
            using (var context = ContextFactory.GetDbContext(_dbConfiguration))
            {
                var data = new TaskInstance();
                data.StartDate = DateTime.Now;
                data.Task = context.Tasks.First(x => x.Id == parentTaskId);
                data.User = context.Users.First(x => x.UserName == userName);

                context.TaskInstances.Add(data);
                context.SaveChanges();

                return CommonResult<int>.Success(data.Id);
            }
        }

        public CommonResult MarkTaskInstanceAsFinished(int parentTaskId)
        {
            using (var context = ContextFactory.GetDbContext(_dbConfiguration))
            {
                var data = context.TaskInstances.First(x => x.Task.Id == parentTaskId && x.User.UserName == StaticManager.UserName);
                data.IsFinished = true;
                data.FinishDate = DateTime.Now;

                context.SaveChanges();

                return CommonResult.Success();
            }
        }

        public CommonResult CreateCodeReview(CodeReviewData requestData)
        {
            using (var context = ContextFactory.GetDbContext(_dbConfiguration))
            {
                var data = new TaskInstanceReview();
                data.ArchitectureRate = requestData.ArchitectureRate;
                data.CleanCodeRate = requestData.CleanCodeRate;
                data.Comment = requestData.Comment;
                data.OptymizationRate = requestData.OptymizationRate;

                var taskInstance = context.TaskInstances.First(x => x.Id == requestData.TaskInstanceId);
                taskInstance.CodeReviewExist = true;

                data.TaskInstance = taskInstance;

                context.TaskInstanceReviews.Add(data);
                context.SaveChanges();

                return CommonResult.Success();
            }
        }
    }
}