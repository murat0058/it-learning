using ITLearning.Contract.Enums;
using System.Collections.Generic;
using System.Security.Claims;

namespace ITLearning.Frontend.Web.Core.Identity.Providers
{
    public class StartupPermissionsProvider : IStartupPermissionsProvider
    {
        public IEnumerable<Claim> GetStartupClaims()
        {
            return new List<Claim>
            {
                CreateClaimByClaimEnums(ClaimTypeEnum.Controller, ClaimValueEnum.Controller_HomeController),
                CreateClaimByClaimEnums(ClaimTypeEnum.Controller, ClaimValueEnum.Controller_TasksController),
                CreateClaimByClaimEnums(ClaimTypeEnum.Controller, ClaimValueEnum.Controller_NewsController),
                CreateClaimByClaimEnums(ClaimTypeEnum.Controller, ClaimValueEnum.Controller_GroupsController)
            };
        }

        public IEnumerable<string> GetStartupRoles()
        {
            return new List<string>
            {
                UserRoleEnum.StandardUser.ToString()
            };
        }

        private Claim CreateClaimByClaimEnums(ClaimTypeEnum type, ClaimValueEnum value)
        {
            return new Claim(type.ToString(), value.ToString());
        }
    }
}