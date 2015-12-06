using CommonMark;
using ITLearning.Contract.Data.Model;
using ITLearning.Contract.Data.Model.Branches;
using ITLearning.Contract.Data.Model.Groups;
using ITLearning.Contract.Data.Model.Tasks;
using ITLearning.Contract.Data.Model.User;
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

        [HttpGet("{id:int}")]
        public IActionResult Single(int id)
        {
            //todo get taskVieType by id && user
            var taskViewType = TaskViewTypeEnum.OwnerView;

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
            var task = new TaskData()
            {
                Id = 1,
                Title = "Nowe zadanie #1",
                Description = CommonMarkConverter.Convert("Zaczynamy! **Przykładowy opis 1,2,3...**"),
                IsActive = true,
                SelectedLanguage = LanguageEnum.CSharp,
                UserGroup = new UserGroupData()
                {
                    Id = 32,
                    Name = "Ludzie i c#"
                },
                Branches = new List<BranchShortData>
                {
                    new BranchShortData()
                    {
                        Name = "master",
                        Description = "Główny branch. Pobież kod, od którego możesz zacząć!"
                    },
                    new BranchShortData()
                    {
                        Name = "Podpowiedź 1",
                        Description = "Pierwsza podpowiedź"
                    }
                },
                RepositoryLink = "https:/azure.git/asdsadsadds/",
                TaskInstances = new List<TaskInstanceData>
                {
                    new TaskInstanceData()
                    {
                        User = new UserShortData
                        {
                            Id = 1,
                            UserName = "Ziutek"
                        },
                        Finished = false,
                        CreateDate = "2015-11-23",
                        CodeReviewExist = false
                    },
                    new TaskInstanceData()
                    {
                        User = new UserShortData
                        {
                            Id = 1,
                            UserName = "Zdzisek"
                        },
                        Finished = true,
                        CreateDate = "2015-11-24",
                        FinishDate = "2015-11-29",
                        CodeReviewExist = false
                    },
                    new TaskInstanceData()
                    {
                        User = new UserShortData
                        {
                            Id = 1,
                            UserName = "Zbigniew"
                        },
                        Finished = true,
                        CreateDate = "2015-11-21",
                        FinishDate = "2015-11-26",
                        CodeReviewExist = true
                    }
                }
            };

            return View("SingleOwner", task);
        }

        [HttpGet("InstanceSingleView/{id:int}")]
        public IActionResult InstanceSingleView(int id)
        {
            var task = new TaskData()
            {
                Id = 1,
                Title = "Nowe zadanie #1",
                Description = CommonMarkConverter.Convert("Zaczynamy! **Przykładowy opis 1,2,3...**"),
                IsActive = true,
                SelectedLanguage = LanguageEnum.CSharp,
                UserGroup = new UserGroupData()
                {
                    Id = 32,
                    Name = "Ludzie i c#"
                },
                Branches = new List<BranchShortData>
                {
                    new BranchShortData()
                    {
                        Name = "master",
                        Description = "Główny branch. Pobież kod, od którego możesz zacząć!"
                    },
                    new BranchShortData()
                    {
                        Name = "Podpowiedź 1",
                        Description = "Pierwsza podpowiedź"
                    }
                },
                Author = new UserShortData()
                {
                    Id = 5,
                    UserName = "Adrianno"
                }
            };

            return View("SingleInstance", task);
        }

        [HttpGet("PublicSingleView/{id:int}")]
        public IActionResult PublicSingleView(int id)
        {
            var task = new TaskData()
            {
                Id = 1,
                Title = "Nowe zadanie #1",
                Description = CommonMarkConverter.Convert("Zaczynamy! **Przykładowy opis 1,2,3...**"),
                IsActive = true,
                SelectedLanguage = LanguageEnum.CSharp,
                UserGroup = new UserGroupData()
                {
                    Id = 32,
                    Name = "Ludzie i c#"
                },
                Branches = new List<BranchShortData>
                {
                    new BranchShortData()
                    {
                        Name = "master",
                        Description = "Główny branch. Pobież kod, od którego możesz zacząć!"
                    },
                    new BranchShortData()
                    {
                        Name = "Podpowiedź 1",
                        Description = "Pierwsza podpowiedź"
                    }
                },
                Author = new UserShortData()
                {
                    Id = 5,
                    UserName = "Adrianno"
                }
            };

            return View("Single", task);
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

        [HttpPost("BeginTask/{taskId:int}")]
        public IActionResult BeginTask(int taskId)
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
                new TaskListItemData() { Id = 0, Name = "Wprowadzenie do C#", GroupName = "Ludzie i c#", IsCompleted = false, Language = LanguageEnum.CSharp.ToString(), GroupId = 1 },
                new TaskListItemData() { Id = 1, Name = "Wprowadzenie do JS", GroupName = "Ludzie i js", IsCompleted = false, Language = LanguageEnum.JavaScript.ToString(), GroupId = 1 },
                new TaskListItemData() { Id = 2, Name = "Wprowadzenie do JAVA", GroupName = "Ludzie i java", IsCompleted = true, Language = LanguageEnum.Other.ToString(), GroupId = 1 }
            };

            var result = CommonResult<List<TaskListItemData>>.Success(tasks);

            return new JsonResult(result);
        }
    }
}