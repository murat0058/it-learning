using ITLearning.Frontend.Web.Core.Identity.Enums;
using System.Collections.Generic;
using System.Security.Claims;

namespace ITLearning.Frontend.Web.Core.Identity.Providers
{
    public class PermissionsProvider : IPermissionsProvider
    {
        public IEnumerable<Claim> GetBasicClaims()
        {
            return new List<Claim>
            {
                GetClaimForTypeAndValue(ClaimTypeEnum.Controller, ClaimValueEnum.HomeController)
            };
        }

        public IEnumerable<string> GetBasicRoles()
        {
            return new List<string>
            {
                "StandardUser"
            };
        }

        private Claim GetClaimForTypeAndValue(ClaimTypeEnum type, ClaimValueEnum value)
        {
            return new Claim(type.ToString(), value.ToString());
        }
    }
}