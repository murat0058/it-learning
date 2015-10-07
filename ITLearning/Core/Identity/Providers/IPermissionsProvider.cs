using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ITLearning.Frontend.Web.Core.Identity.Providers
{
    public interface IPermissionsProvider
    {
        IEnumerable<Claim> GetBasicClaims();
        IEnumerable<string> GetBasicRoles();
    }
}
