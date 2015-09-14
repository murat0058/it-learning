using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Frontend.Web.Core.Identity.Common
{
    public static class IdentityRouteValues
    {
        public static string UnauthorizedControllerRoute { get; } = "Account";
        public static string UnauthorizedActionRoute { get; } = "Unauthorized";
    }
}
