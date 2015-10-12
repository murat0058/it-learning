using ITLearning.Frontend.Web.Core.Identity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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

        #region Helpers
        private Claim GetClaimForTypeAndValue(ClaimTypeEnum type, ClaimValueEnum value)
        {
            return new Claim(type.ToString(), value.ToString());
        }
        #endregion
    }
}
