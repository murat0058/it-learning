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
using ITLearning.Frontend.Web.Core.Identity.Attributes;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ITLearning.Frontend.Web.Controllers
{
    [Route("Tasks")]
    [AuthorizeClaim(Type = ClaimTypeEnum.Controller, Value = ClaimValueEnum.Controller_TasksController)]
    public class TasksController : BaseController
    {
        private readonly ITasksService _tasksService;
        private readonly IApplicationEnvironment _hostingEnvironment;

        public TasksController(ITasksService tasksService, IApplicationEnvironment hostingEnvironment)
        {
            _tasksService = tasksService;
            _hostingEnvironment = hostingEnvironment;

        }

        [HttpGet("{id:int}")]
        public IActionResult Single(int id)
        {
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
                        Description = "Główny branch. Pobierz kod, od którego możesz zacząć!",
                    },
                    new BranchShortData()
                    {
                        Name = "Podpowiedź 1",
                        Description = "Pierwsza podpowiedź",
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
                        CodeReviewExist = false,
                        RepositoryLink = "http:/itlearning.com/repozad1.git",
                        CodeReview = new CodeReviewData
                        {
                            NumberOfActivityDays = 5,
                            ArchitectureRate = 100,
                            CleanCodeRate = 60,
                            OptymizationRate = 0,
                            Comment = "Oby tak dalej!",
                            Branches = new List<BranchShortData>
                            {
                                new BranchShortData()
                                {
                                    Name = "master",
                                    Description = "Główny branch. Pobierz kod, od którego możesz zacząć!",
                                    Visible = true
                                },
                                new BranchShortData()
                                {
                                    Name = "Podp.1",
                                    Description = "Pierwsza podpowiedź",
                                    Visible = false
                                }
                            }
                        }
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
                        CodeReviewExist = false,
                        RepositoryLink = "http:/itlearning.com/repozad2.git",
                        CodeReview = new CodeReviewData
                        {
                            NumberOfActivityDays = 5,
                            //ArchitectureRate = 100,
                            //CleanCodeRate = 50,
                            //OptymizationRate = 0,
                            //Comment = "Oby tak dalej!",
                            Branches = new List<BranchShortData>
                            {
                                new BranchShortData()
                                {
                                    Name = "master",
                                    Description = "Główny branch. Pobierz kod, od którego możesz zacząć!",
                                    Visible = true
                                },
                                new BranchShortData()
                                {
                                    Name = "Podp.1",
                                    Description = "Pierwsza podpowiedź",
                                    Visible = false
                                }
                            }
                        }
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
                        CodeReviewExist = true,
                        RepositoryLink = "http:/itlearning.com/repozad3.git",
                        CodeReview = new CodeReviewData
                        {
                            NumberOfActivityDays = 5,
                            ArchitectureRate = 100,
                            CleanCodeRate = 60,
                            OptymizationRate = 0,
                            Comment = "Oby tak dalej!",
                            Branches = new List<BranchShortData>
                            {
                                new BranchShortData()
                                {
                                    Name = "master",
                                    Description = "Główny branch. Pobierz kod, od którego możesz zacząć!",
                                    Visible = true
                                },
                                new BranchShortData()
                                {
                                    Name = "Podp.1",
                                    Description = "Pierwsza podpowiedź",
                                    Visible = false
                                }
                            }
                        }
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
                        Description = "Główny branch. Pobierz kod, od którego możesz zacząć!",
                        Visible = true
                    },
                    new BranchShortData()
                    {
                        Name = "Podpowiedź 1",
                        Description = "Pierwsza podpowiedź",
                        Visible = false
                    }
                },
                Author = new UserShortData()
                {
                    Id = 5,
                    UserName = "Adrianno"
                },
                RepositoryLink = "https:/azure.git/asdsadsadds/",
                CreationDate = DateTime.Now.ToShortDateString(),
                FinishDate = DateTime.Now.ToShortDateString(),
                Finished = true,
                CodeReviewExist = true,
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
                        CodeReviewExist = false,
                        RepositoryLink = "http:/itlearning.com/repozad1.git",
                        CodeReview = new CodeReviewData
                        {
                            ArchitectureRate = 100,
                            CleanCodeRate = 60,
                            OptymizationRate = 0,
                            Comment = "Oby tak dalej!",
                        }
                    }
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
                        Description = "Główny branch. Pobierz kod, od którego możesz zacząć!"
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
        [AuthorizeClaim(Type = ClaimTypeEnum.Task, Value = ClaimValueEnum.Task_Create)]
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
        [AuthorizeClaim(Type = ClaimTypeEnum.Task, Value = ClaimValueEnum.Task_Create)]
        public IActionResult Create(CreateTaskRequestData requestData)
        {
            //_tasksService.Create(requestData);

            return RedirectToAction("ActionName");
        }

        [HttpGet("Edit/{id:int}")]
        public IActionResult Edit(int id)
        {
            var viewModel = new EditTaskRequestData()
            {
                Title = "Nowe zadanie 1",
                Description = "Zaczynamy! **Przykładowy opis 1,2,3...**",
                IsActive = false,
                RepositoryLink = "http:/itlearning.com/repozad3.git",
                SelectedLanguage = new EnumDisplayData()
                {
                    Id = 2,
                    Name = LanguageEnum.CSharp.ToString()
                },
                Branches= new List<BranchShortData>
                {
                    new BranchShortData()
                    {
                        Name = "master",
                        Description = "Główny branch. Nie można go usunąć."
                    },
                    new BranchShortData()
                    {
                        Name = "podp1",
                        Description = "Podpowiedź 1."
                    }
                },
                SelectedGroup = new UserGroupData() { Name = "Brak", Id = -1 },
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

            return View("Edit", JsonConvert.SerializeObject(viewModel));
        }

        [HttpPost("Edit")]
        public IActionResult Edit(EditTaskRequestData requestData)
        {
            return RedirectToAction("ActionName");
        }

        [HttpGet("Delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            var deleteTaskData = new DeleteTaskRequestData()
            {
                Id = id
            };

            return View(deleteTaskData);
        }

        [HttpPost("Delete/{id:int}")]
        public IActionResult DeletePost(int id)
        {
            return RedirectToAction("Action");
        }

        [HttpPost("BeginTask/{id:int}")]
        public IActionResult BeginTask(int id)
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

        [HttpPost("ShowBranch/{taskInstanceId:int}/{branchName}")]
        public IActionResult ShowBranch(int taskInstanceId, string branchName)
        {

            return RedirectToAction("ActionName");
        }
    }
}