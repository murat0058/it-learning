using ITLearning.Contract.Data.Model.CodeReview;
using ITLearning.Contract.Data.Requests.Tasks;
using ITLearning.Contract.Enums;
using ITLearning.Contract.Services;
using ITLearning.Frontend.Web.Controllers.Base;
using ITLearning.Frontend.Web.Core.Identity.Attributes;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using System;

namespace ITLearning.Frontend.Web.Controllers
{
    [Route("Tasks")]
    [AuthorizeClaim(Type = ClaimTypeEnum.Controller, Value = ClaimValueEnum.Controller_TasksController)]
    public class TasksController : BaseController
    {
        private readonly ITasksService _tasksService;
        private readonly IGroupsService _groupsService;

        public TasksController(ITasksService tasksService, IGroupsService groupsService)
        {
            _tasksService = tasksService;
            _groupsService = groupsService;
        }

        [HttpGet("{id:int}")]
        public IActionResult Single(int id)
        {
            var taskViewType = _tasksService.GetViewType(id);

            if (taskViewType == TaskViewTypeEnum.OwnerView)
            {
                return RedirectToAction("OwnerSingleView", new { id = id });
            }

            if (taskViewType == TaskViewTypeEnum.InstanceView)
            {
                return RedirectToAction("InstanceSingleView", new { id = id });
            }

            return RedirectToAction("PublicSingleView", new { id = id });
        }

        [HttpGet("OwnerSingleView/{id:int}")]
        public IActionResult OwnerSingleView(int id)
        {
            var data = _tasksService.GetOwnerTaskData(id);

            return View("SingleOwner", data);
        }

        [HttpGet("InstanceSingleView/{id:int}")]
        public IActionResult InstanceSingleView(int id)
        {
            var data = _tasksService.GetInstanceTaskData(id);

            return View("SingleInstance", data);
        }

        [HttpGet("PublicSingleView/{id:int}")]
        public IActionResult PublicSingleView(int id)
        {
            var data = _tasksService.GetTaskData(id);

            return View("Single", data);
        }

        [HttpGet("Create")]
        [AuthorizeClaim(Type = ClaimTypeEnum.Task, Value = ClaimValueEnum.Task_Create)]
        public IActionResult Create()
        {
            var data = _tasksService.GetDataForCreate();

            return View("Create", JsonConvert.SerializeObject(data));
        }

        [HttpGet("Edit/{id:int}")]
        public IActionResult Edit(int id)
        {
            var data = _tasksService.GetDataForEdit(id);

            return View("Edit", JsonConvert.SerializeObject(data));
        }

        [HttpGet("Delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            var data = _tasksService.GetDataForDelete(id);

            return View(data);
        }

        [HttpPost("GetLatest")]
        public IActionResult GetLatest()
        {
            var result = _tasksService.GetList(3, true);

            return new JsonResult(result);
        }

        [HttpGet("TasksList")]
        public IActionResult TasksList()
        {
            return View("List");
        }

        [HttpPost("GetList")]
        public IActionResult GetList(GetTasksListRequestData requestData)
        {
            var result = _tasksService.GetList(requestData);

            return new JsonResult(result);
        }

        [HttpPost("GetTasksForUser/{userName}")]
        public IActionResult GetTasksForUser(string userName)
        {
            var result = _tasksService.GetList(null, true, userName);
            result.ErrorMessage = "Brak zadań.";

            return new JsonResult(result);
        }

        [HttpPost("Create")]
        [AuthorizeClaim(Type = ClaimTypeEnum.Task, Value = ClaimValueEnum.Task_Create)]
        public IActionResult Create(CreateTaskRequestData requestData)
        {
            var result = _tasksService.Create(requestData);

            return Json(result);
        }

        [HttpGet("Save/{id}/{activate}")]
        [AuthorizeClaim(Type = ClaimTypeEnum.Task, Value = ClaimValueEnum.Task_Create)]
        public IActionResult Save(string id, string activate)
        {
            if (Convert.ToBoolean(activate))
            {
                var result = _tasksService.Activate(Convert.ToInt32(id));
            }

            return RedirectToAction("Single", new { id = id });
        }

        [HttpPost("Edit")]
        public IActionResult Edit(EditTaskRequestData requestData)
        {
            var result = _tasksService.Update(requestData);

            return RedirectToAction("Single", new { id = requestData.Id });
        }

        [HttpPost("Delete/{id:int}")]
        public IActionResult DeletePost(int id)
        {
            var result = _tasksService.Delete(id);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost("BeginTask/{id:int}")]
        public IActionResult BeginTask(int id)
        {
            var result = _tasksService.BeginTask(id);

            return RedirectToAction("Single", new { id = id });
        }

        [HttpGet("FinishTask/{id:int}")]
        public IActionResult FinishTask(int id)
        {
            var result = _tasksService.FinishTask(id);

            return RedirectToAction("Single", new { id = id });
        }

        [HttpPost("ShowBranch/{taskInstanceId:int}/{branchName}")]
        public IActionResult ShowBranch(int taskInstanceId, string branchName)
        {
            var result = _tasksService.ShowBranch(taskInstanceId, branchName);

            return Json(result);
        }

        [HttpPost("CreateBranch")]
        public IActionResult CreateBranch(EditTaskRequestData requestData)
        {
            var result = _tasksService.CreateBranch(requestData.Id, requestData.Name, requestData.Description);

            return Json(result);
        }

        [HttpPost("DeleteBranch/{taskId:int}/{branchName}")]
        public IActionResult DeleteBranch(int taskId, string branchName)
        {
            var result = _tasksService.DeleteBranch(taskId, branchName);

            return Json(result);
        }

        [HttpPost("CreateCodeReview")]
        public IActionResult CreateCodeReview(CodeReviewData requestData)
        {
            var result = _tasksService.CreateCodeReview(requestData);

            return Json(result);
        }
    }
}