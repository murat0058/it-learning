﻿using AutoMapper;
using ITLearning.Contract.Data.Model.Administration;
using ITLearning.Contract.Enums;
using ITLearning.Contract.Services;
using ITLearning.Frontend.Web.Controllers.Base;
using ITLearning.Frontend.Web.Core.Identity.Attributes;
using ITLearning.Frontend.Web.ViewModels.Administration;
using ITLearning.Shared;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc;
using System.Linq;

namespace ITLearning.Frontend.Web.Controllers
{
    [Route("Administration")]
    [AuthorizeClaim(Type = ClaimTypeEnum.Controller, Value = ClaimValueEnum.Controller_AdministrationController)]
    public class AdministrationController : BaseController
    {
        private IPermissionsService _permissionsService;
        private IUserService _userService;
        private IHostingEnvironment _hostingEnv;

        public AdministrationController(IPermissionsService permissionsService, IHostingEnvironment hostingEnv, IUserService userService)
        {
            _permissionsService = permissionsService;
            _userService = userService;
            _hostingEnv = hostingEnv;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return RedirectToAction("Users");
        }

        [HttpGet("Users")]
        public IActionResult Users()
        {
            ViewBag.HostingEnvironment = _hostingEnv.EnvironmentName;

            var users = _userService.GetAllUsersProfileData().Item;

            users = users.Where(x => x.UserName != StaticManager.UserName);

            var viewModel = new UsersListViewModel
            {
                Users = users
            };

            return View(viewModel);
        }

        [HttpGet("ManageUser/{id}")]
        public IActionResult ManageUser(string id)
        {
            var userResult = _userService.GetUserById(int.Parse(id));

            if (userResult.IsSuccess)
            {
                var user = userResult.Item;

                var claimsData = _permissionsService.GetClaimsForUser(userResult.Item.UserName).Item;

                var viewModel = new UserClaimsViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Claims = Mapper.Map<ClaimsViewModel>(claimsData)
                };

                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Users");
            }
        }

        [HttpPost("UpdateClaims")]
        public IActionResult UpdateClaims(UpdateClaimsViewModel viewModel)
        {
            var claims = viewModel.Claims;

            _permissionsService.UpdateClaims(viewModel.UserName, Mapper.Map<ClaimsData>(claims));

            return RedirectToAction("ManageUser", new { id = viewModel.Id });
        }
    }
}
