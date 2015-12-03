using ITLearning.Contract.Data.Model;
using ITLearning.Contract.Data.Model.Groups;
using ITLearning.Contract.Data.Model.Tasks;
using ITLearning.Contract.Data.Requests.Tasks;
using ITLearning.Contract.Data.Results;
using ITLearning.Contract.Enums;
using ITLearning.Contract.Services;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ITLearning.Frontend.Web.Controllers
{
    [Route("Tasks")]
    public class TasksController : BaseController
    {
        private readonly ITasksService _tasksService;

        public TasksController(ITasksService tasksService)
        {
            _tasksService = tasksService;
        }

        [HttpGet("TasksList")]
        public IActionResult List()
        {
            return View();
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            var viewModel = new CreateTaskRequestData()
            {
                UserGroups = new List<UserGroupData>()
                {
                    new UserGroupData() { Name = "Brak", Id = -1 },
                    new UserGroupData() { Name = "GrupaMojaJeden", Id = 1 }
                },
                AvailableLanguages = new List<EnumDisplayData>()
                {
                    new EnumDisplayData()
                    {
                        Id = (int)LanguageEnum.Other,
                        Name = LanguageEnum.Other.ToString()
                    },
                    new EnumDisplayData() {
                        Id = (int)LanguageEnum.CSharp,
                        Name = LanguageEnum.CSharp.ToString()
                    },
                    new EnumDisplayData() {
                        Id = (int)LanguageEnum.JavaScript,
                        Name = LanguageEnum.JavaScript.ToString()
                    }
                }
            };

            return View("Create", JsonConvert.SerializeObject(viewModel));
        }

        [HttpPost("Create")]
        public IActionResult Create(CreateTaskRequestData requestData)
        {
            //_tasksService.Create(requestData);

            return RedirectToAction("ActionName");
        }

        [HttpPost("GetLatest")]
        public IActionResult GetLatest()
        {
            //var tasks _tasksService.GetLatest(3);

            var tasks = new List<TaskListItemData>
            {
                new TaskListItemData() {Id = 0, Name = "Wprowadzenie do C#", GroupName = "Ludzie i c#", IsCompleted = false, Language = LanguageEnum.CSharp.ToString(), GroupId = 1 },
                new TaskListItemData() {Id = 1, Name = "Wprowadzenie do JS", GroupName = "Ludzie i js", IsCompleted = false, Language = LanguageEnum.JavaScript.ToString(), GroupId = 2 },
                new TaskListItemData() {Id = 2, Name = "Wprowadzenie do JAVA", GroupName = "Ludzie i java", IsCompleted = true, Language = LanguageEnum.Other.ToString(), GroupId = 3 }
            };

            var result = CommonResult<List<TaskListItemData>>.Success(tasks);

            return new JsonResult(result);
        }
    }
}