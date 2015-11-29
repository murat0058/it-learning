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
            var getGroupResult = _groupsService.GetGroupById(id);
            var getAccessTypeResult =_groupsService.GetUserAccessType(id, User.Identity.Name);
            
            if(!getGroupResult.IsSuccess || !getAccessTypeResult.IsSuccess)
            { 
                return RedirectToAction("Index", "Home"); 
            }
            
            var accessTypeEnum = getAccessTypeResult.Item;
            
            if(accessTypeEnum == GroupAccessTypeEnum.RequirePassword)
            {
                return RedirectToAction("ConfirmAccess", new { groupId = id});
            }
            
            var viewModel = Mapper.Map<SingleGroupViewModel>(getGroupResult);
            
            viewModel.AccessType = accessTypeEnum;
            
            //TODO dodatkowe funkcje if owner itd
                        
            return View();
        }
        
        [HttpGet("ConfirmAccess")]
        public IActionResult ConfirmAccess(int groupId)
        {
            var getGroupResult = _groupsService.GetGroupById(groupId);
            var getAccessTypeResult =_groupsService.GetUserAccessType(groupId, User.Identity.Name);
            
            var accessTypeEnum = getAccessTypeResult.Item;
            
            if(accessTypeEnum != GroupAccessTypeEnum.RequirePassword)
            {
                return RedirectToAction("Single", new {id = groupId});    
            }
            
            var groupBasicData = getGroupResult.Item;
            
            var vm = Mapper.Map<ConfirmGroupAccessViewModel>(groupBasicData);
            
            return View(vm);
        }
        
        [HttpPost("ConfirmAccess")]
        public IActionResult ConfirmGroupAccess(GroupAccessRequestViewModel viewModel)
        {
            viewModel.UserName = User.Identity.Name;
            var updateGroupAccessResult = _groupsService.UpdateGroupAccess(Mapper.Map<GroupAccessUpdateRequestData>(viewModel));
            
            if(updateGroupAccessResult.IsSuccess)
            {
                return RedirectToAction("Single", new { id = viewModel.GroupId });   
            }
            else
            {
                ModelState.AddModelError("", updateGroupAccessResult.ErrorMessage);
                return View("ConfirmAccess", viewModel);
            }
        }

        [HttpPost("UserGroupsBasicData")]
        public IActionResult GetUserGroupsBasicData(int noOfGroups)
        {
            var result = _groupsService.GetGroupsBasicDataLimitedByNo(User.Identity.Name, noOfGroups);

            return new JsonResult(result);
        }
    }
}
