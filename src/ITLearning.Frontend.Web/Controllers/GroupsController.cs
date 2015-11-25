using AutoMapper;
using ITLearning.Contract.Data.Requests;
using ITLearning.Contract.Data.Results;
using ITLearning.Contract.Data.Results.Groups;
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
                ModelState.AddModelError(string.Empty, result.ErrorMessage);

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
            var result = _groupsService.GetGroupBasicData(User.Identity.Name, noOfGroups);

            return new JsonResult(result);
        }
    }
}
