using ITLearning.Frontend.Web.Core.Identity.Enums;
using System.Collections.Generic;
using System.Security.Claims;

namespace ITLearning.Frontend.Web.Core.Identity.Providers
{
    public class PermissionsProvider : IPermissionsProvider
    {
        public IEnumerable<Claim> GetStartupClaims()
        {
            return new List<Claim>
            {
                CreateClaimByClaimEnums(ClaimTypeEnum.Controller, ClaimValueEnum.Controller_HomeController),
                CreateClaimByClaimEnums(ClaimTypeEnum.Controller, ClaimValueEnum.Controller_NewsController),
                CreateClaimByClaimEnums(ClaimTypeEnum.Controller, ClaimValueEnum.Controller_GroupController),

                //TODO To be removed
                CreateClaimByClaimEnums(ClaimTypeEnum.UserWidgetTab, ClaimValueEnum.UserWidgetTab_ProfileEditing),
                CreateClaimByClaimEnums(ClaimTypeEnum.UserWidgetTab, ClaimValueEnum.UserWidgetTab_HomeSettings),
            };
        }

        public IEnumerable<string> GetStartupRoles()
        {
            return new List<string>
            {
                "StandardUser"
            };
        }

        private Claim CreateClaimByClaimEnums(ClaimTypeEnum type, ClaimValueEnum value)
        {
            return new Claim(type.ToString(), value.ToString());
        }
    }
}