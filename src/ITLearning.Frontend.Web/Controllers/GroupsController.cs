using AutoMapper;
using ITLearning.Contract.Data.Requests;
using ITLearning.Contract.Data.Results;
using ITLearning.Contract.Services;
using ITLearning.Frontend.Web.Core.Identity.Attributes;
using ITLearning.Frontend.Web.Core.Identity.Enums;
using ITLearning.Frontend.Web.ViewModels.Group;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Frontend.Web.Controllers
{
    [Route("Groups")]
    [AuthorizeClaim(Type = ClaimTypeEnum.Controller, Value = ClaimValueEnum.Controller_GroupsController)]
    public class GroupsController : BaseController
    {
        private IGroupsService _groupsService;

        public GroupsController(IGroupsService groupsService)
        {
            _groupsService = groupsService;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View(new CreateGroupViewModel());
        }

        [HttpPost("CreateGroup")]
        public IActionResult CreateGroup(CreateGroupViewModel viewModel)
        {
            var request = Mapper.Map<CreateGroupRequestData>(viewModel);
            request.UserName = User.Identity.Name;

            var result = _groupsService.CreateGroup(request);

            if (result.IsSuccess)
            {
                return RedirectToAction("Single", routeValues: new { id = result.Item.Id });
            }
            else
            {
                ModelState.AddModelError("Error", result.ErrorMessage);
                return View("Create", viewModel);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Single(int id)
        {
            return View();
        }

        [HttpPost("UserGroupsBasicData")]
        public IActionResult GetUserGroupsBasicData(int noOfGroups)
        {
            var result = CommonResult<IEnumerable<GroupBasicDataViewModel>>.Success(new List<GroupBasicDataViewModel>
            {
                new GroupBasicDataViewModel
                {
                    Id = 0,
                    IsPrivate = true,
                    Name = "Grupa testowa 1",
                    NoOfUsers = 1
                },
                new GroupBasicDataViewModel
                {
                    Id = 1,
                    IsPrivate = false,
                    Name = "Grupa testowa z nazwą która powina się obciąć test 123",
                    NoOfUsers = 20
                },
                new GroupBasicDataViewModel
                {
                    Id = 3,
                    IsPrivate = false,
                    Name = "Grupa testowa 3",
                    NoOfUsers = 99999
                },
                new GroupBasicDataViewModel
                {
                    Id = 4,
                    IsPrivate = true,
                    Name = "Grupa testowa 1",
                    NoOfUsers = 1501
                },
                new GroupBasicDataViewModel
                {
                    Id = 5,
                    IsPrivate = false,
                    Name = "Grupa testowa z długo nazwo",
                    NoOfUsers = 3
                },
                new GroupBasicDataViewModel
                {
                    Id = 6,
                    IsPrivate = false,
                    Name = "Grupa testowa 3",
                    NoOfUsers = 15
                }
            });

            return new JsonResult(result.Item.Take(noOfGroups));
        }
    }
}
