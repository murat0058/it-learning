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

            var groupResult = _groupsService.GetFullData(new GetGroupRequest { GroupId = groupId });

            if (accessTypeResult.IsSuccess && groupResult.IsSuccess)
            {
                var accessType = accessTypeResult.Item.GroupAccessTypeEnum;

                if (accessType == GroupAccessTypeEnum.RequirePassword)
                {
                    return RedirectToAction("Password", new { groupId = groupId });
                }

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

        [HttpGet("Password/{groupId}")]
        public IActionResult Password(int groupId)
        {
            var groupResult = _groupsService.GetDataWithUsers(new GetGroupRequest { GroupId = groupId });

            if (groupResult.IsSuccess)
            {
                var basicDataViewModel = Mapper.Map<GroupBasicDataViewModel>(groupResult.Item);

                var entryViewModel = new PasswordEntryViewModel
                {
                    BasicDataViewModel = basicDataViewModel,
                    PasswordEntryDataViewModel = new PasswordEntryDataViewModel
                    {
                        GroupId = basicDataViewModel.Id
                    }
                };

                return View("Password", entryViewModel);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost("PasswordEntry")]
        public IActionResult PasswordEntry(PasswordEntryViewModel viewModel)
        {
            var vm = viewModel.PasswordEntryDataViewModel;

            var result = _groupsService.TryAddUserToPrivateGroup(new AddUserToGroupRequest
            {
                GroupId = vm.GroupId,
                UserName = User.Identity.Name,
                Password = vm.Password
            });

            if (result.IsSuccess)
            {
                return RedirectToAction("Single", new { groupId = vm.GroupId });
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

        [HttpPost("GetGroupsList")]
        public IActionResult GetGroupsList(GetGroupsListViewModel model)
        {
            //var request = new GetLatestGroupsBasicDataRequest
            //{
            //    UserName = User.Identity.Name,
            //    NoOfGroups = noOfGroups
            //};

            //var result = _groupsService.GetLatestGroupsData(request);

            return new JsonResult("test");
        }

        [HttpPost("GetUsersForGroup")]
        public IActionResult GetUsersForGroup(UsersForGroupViewModel viewModel)
        {
            var request = new GetUsersForGroupRequest
            {
                OwnerName = User.Identity.Name,
                GroupId = viewModel.GroupId
            };

            var result = _groupsService.GetUsersForGroup(request);

            return new JsonResult(result);
        }

        [HttpPost("DeleteUser")]
        public IActionResult DeleteUser(DeleteUserFromGroupViewModel viewModel)
        {
            var request = new UserGroupRequest
            {
                GroupId = viewModel.GroupId,
                Users = new int[] { viewModel.UserId }
            };

            var result = _groupsService.RemoveUsers(request);

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
