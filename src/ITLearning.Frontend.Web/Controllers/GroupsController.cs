using AutoMapper;
using ITLearning.Contract.Data.Requests.Groups;
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
using ITLearning.Contract.Enums;

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
            var request = Mapper.Map<CreateGroupRequest>(viewModel);
            request.UserName = User.Identity.Name;

            var result = _groupsService.CreateGroup(request);

            if (result.IsSuccess)
            {
                return RedirectToAction("Single", routeValues: new { groupId = result.Item.Id });
            }
            else
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage);

                return View("Create", viewModel);
            }
        }

        [HttpGet("Manage/{groupId}")]
        public IActionResult Manage(int groupId)
        {
            if (!CheckIfOwner(groupId, User.Identity.Name))
            {
                return RedirectToAction("Index");
            }

            var groupResult = _groupsService.GetDataWithUsers(new GetGroupRequest { GroupId = groupId });

            var basicDataViewModel = Mapper.Map<GroupBasicDataViewModel>(groupResult.Item);
            var updateGroupViewModel = Mapper.Map<UpdateGroupViewModel>(groupResult.Item);

            var viewModel = new ManageGroupViewModel
            {
                GroupId = groupResult.Item.Id,
                BasicDataViewModel = basicDataViewModel,
                UpdateGroupViewModel = updateGroupViewModel,
                AccessType = GroupAccessTypeEnum.Owner
            };

            return View("Manage", viewModel);
        }

        [HttpPost("Delete/{groupId}")]
        public IActionResult DeleteGroup(int groupId)
        {
            if (!CheckIfOwner(groupId, User.Identity.Name))
            {
                return RedirectToAction("Index");
            }

            var request = new DeleteGroupRequest
            {
                GroupId = groupId,
                UserName = User.Identity.Name
            };

            var deleteGroupResult = _groupsService.DeleteGroup(request);

            if (deleteGroupResult.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Single", new { groupId = groupId });
            }
        }

        [HttpGet("{groupId}")]
        public IActionResult Single(int groupId)
        {
            var accessTypeResult = _groupsService.GetAccessType(new GroupAccessTypeRequest
            {
                GroupId = groupId,
                UserName = User.Identity.Name
            });

            var groupResult = _groupsService.GetDataWithUsers(new GetGroupRequest { GroupId = groupId });

            if (accessTypeResult.IsSuccess && groupResult.IsSuccess)
            {
                var accessType = accessTypeResult.Item.GroupAccessTypeEnum;

                var basicDataViewModel = Mapper.Map<GroupBasicDataViewModel>(groupResult.Item);

                var viewModel = new SingleGroupViewModel
                {
                    GroupId = groupResult.Item.Id,
                    BasicDataViewModel = basicDataViewModel,
                    AccessType = accessType
                };

                return View("Single", viewModel);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost("UpdateGroup")]
        public IActionResult UpdateGroup(ManageGroupViewModel viewModel)
        {
            var groupId = viewModel.UpdateGroupViewModel.Id;

            if (!CheckIfOwner(groupId, User.Identity.Name))
            {
                return RedirectToAction("Index");
            }

            var updateGroupViewModel = viewModel.UpdateGroupViewModel;

            var request = Mapper.Map<UpdateGroupRequest>(updateGroupViewModel);

            var result = _groupsService.UpdateGroup(request);

            return RedirectToAction("Manage", new { groupId = groupId });
        }

        [HttpPost("UserGroupsBasicData")]
        public IActionResult GetUserGroupsBasicData(int noOfGroups)
        {
            var request = new GetLatestGroupsBasicDataRequest
            {
                UserName = User.Identity.Name,
                NoOfGroups = noOfGroups
            };

            var result = _groupsService.GetLatestGroupsData(request);

            return new JsonResult(result);
        }

        private bool CheckIfOwner(int groupId, string userName)
        {
            var accessTypeResult = _groupsService.GetAccessType(new GroupAccessTypeRequest
            {
                GroupId = groupId,
                UserName = userName
            });

            return accessTypeResult.IsSuccess && accessTypeResult.Item.GroupAccessTypeEnum == GroupAccessTypeEnum.Owner;
        }
    }
}
