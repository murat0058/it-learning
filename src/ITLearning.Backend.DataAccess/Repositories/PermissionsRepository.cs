using ITLearning.Backend.Database;
using ITLearning.Backend.Database.Entities;
using ITLearning.Contract.Data.Results;
using ITLearning.Contract.DataAccess.Repositories;
using ITLearning.Contract.Enums;
using ITLearning.Shared.Configs;
using Microsoft.Extensions.OptionsModel;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using ITLearning.Contract.Data.Model.Administration;
using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Http;
using System.Threading;

namespace ITLearning.Backend.DataAccess.Repositories
{
    public class PermissionsRepository : IPermissionsRepository
    {
        private readonly IOptions<DatabaseConfiguration> _dbConfiguration;
        private UserManager<User> _userManager;

        public PermissionsRepository(IOptions<DatabaseConfiguration> dbConfiguration, UserManager<User> userManager)
        {
            _dbConfiguration = dbConfiguration;
            _userManager = userManager;
        }

        public CommonResult<ClaimsData> GetClaimsForUser(string userName)
        {
            using (var context = ContextFactory.GetDbContext(_dbConfiguration))
            {
                var user = context.Users.FirstOrDefault(x => x.UserName == userName);

                if (user == null)
                {
                    return CommonResult<ClaimsData>.Failure("Użytkownik nie istnieje.");
                }

                var claims = context.UserClaims
                    .Where(x => x.UserId == user.Id)
                    .Select(x => new Claim(x.ClaimType, x.ClaimValue));

                var claimsData = new ClaimsData();

                claimsData.ControllerAdministrationController = CheckClaim(claims, ClaimTypeEnum.Controller, ClaimValueEnum.Controller_AdministrationController);
                claimsData.ControllerHomeController = CheckClaim(claims, ClaimTypeEnum.Controller, ClaimValueEnum.Controller_HomeController);
                claimsData.ControllerNewsController = CheckClaim(claims, ClaimTypeEnum.Controller, ClaimValueEnum.Controller_NewsController);
                claimsData.ControllerTasksController = CheckClaim(claims, ClaimTypeEnum.Controller, ClaimValueEnum.Controller_TasksController);
                claimsData.ControllerGroupsController = CheckClaim(claims, ClaimTypeEnum.Controller, ClaimValueEnum.Controller_GroupsController);
                claimsData.TaskCreate = CheckClaim(claims, ClaimTypeEnum.Task, ClaimValueEnum.Task_Create);
                claimsData.NewsCreate = CheckClaim(claims, ClaimTypeEnum.News, ClaimValueEnum.News_Create);
                claimsData.GroupCreate = CheckClaim(claims, ClaimTypeEnum.Group, ClaimValueEnum.Group_Create);

                return CommonResult<ClaimsData>.Success(claimsData);
            }
        }

        public CommonResult AddClaim(string userName, ClaimTypeEnum type, ClaimValueEnum value)
        {
            using (var context = ContextFactory.GetDbContext(_dbConfiguration))
            {
                var user = context.Users
                        .Include(x => x.Claims)
                        .FirstOrDefault(x => x.UserName == userName);


                var claim = user.Claims.FirstOrDefault(x => x.ClaimType == type.ToString() && x.ClaimValue == value.ToString());

                if (claim == null)
                {
                    user.Claims.Add(new IdentityUserClaim<int>
                    {
                        UserId = user.Id,
                        ClaimType = type.ToString(),
                        ClaimValue = value.ToString()
                    });
                }

                context.SaveChanges();
            }

            return CommonResult.Success();
        }

        public CommonResult RemoveClaim(string userName, ClaimTypeEnum type, ClaimValueEnum value)
        {
            using (var context = ContextFactory.GetDbContext(_dbConfiguration))
            {
                var user = context.Users
                    .Include(x => x.Claims)
                    .FirstOrDefault(x => x.UserName == userName);

                var claim = user.Claims.FirstOrDefault(x => x.ClaimType == type.ToString() && x.ClaimValue == value.ToString());

                if (claim != null)
                {
                    user.Claims.Remove(claim);
                }

                context.SaveChanges();
            }

            return CommonResult.Success();
        }

        private bool CheckClaim(IEnumerable<Claim> claims, ClaimTypeEnum type, ClaimValueEnum value)
        {
            return claims.FirstOrDefault(x => x.Type == type.ToString() && x.Value == value.ToString()) != null;
        }
    }
}
