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

            };
        }

        public IEnumerable<string> GetBasicRoles()
        {
            return new List<string>
            {
                "StandardUser"
            };
        }
    }
}
