using ITLearning.Frontend.Web.Contract.Data.Results;
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
    [Route("Group")]
    [AuthorizeClaim(Type = ClaimTypeEnum.Controller, Value = ClaimValueEnum.Controller_GroupController)]
    public class GroupController : BaseController
    {
        [HttpGet("{id}")]
        public IActionResult Single(int id)
        {
            return View();
        }

        [HttpGet("LatestUserGroups")]
        public IActionResult LatestUserGroups()
        {
            var result = CommonResult<IEnumerable<GroupBasicDataViewModel>>.Success(new List<GroupBasicDataViewModel>
            {
                new GroupBasicDataViewModel
                {
                    Id = 0,
                    IsPrivate = true,
                    Name = "Grupa testowa 1",
                    NoOfUsers = 17
                },
                new GroupBasicDataViewModel
                {
                    Id = 1,
                    IsPrivate = false,
                    Name = "Grupa testowa z długo nazwo",
                    NoOfUsers = 20
                },
                new GroupBasicDataViewModel
                {
                    Id = 3,
                    IsPrivate = false,
                    Name = "Grupa testowa 3",
                    NoOfUsers = 15
                }
            });

            return new JsonResult(result.Item);
        }
    }
}
