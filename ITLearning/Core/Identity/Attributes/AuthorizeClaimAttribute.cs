using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Framework.Internal;
using System.Security.Claims;
using System.Threading;
using ITLearning.Frontend.Web.Core.Identity.Enums;
using ITLearning.Frontend.Web.Core.Identity.Common;
using Microsoft.AspNet.Mvc.Filters;

namespace ITLearning.Frontend.Web.Core.Identity.Attributes
{
    public class AuthorizeClaimAttribute : AuthorizationFilterAttribute
    {
        public ClaimType Type { get; set; }
        public ClaimValue Value { get; set; }

        public override Task OnAuthorizationAsync(AuthorizationContext context)
        {
            if(IsClaimAuthorizationRequested(context) && HasUserRequestedClaim(context) == false)
            {
                context.Result = 
                    new RedirectToActionResult(
                        IdentityRouteValues.UnauthorizedActionRoute, 
                        IdentityRouteValues.UnauthorizedControllerRoute, 
                        null);
            }
            
            return Task.FromResult<object>(null);
        }

        private bool IsClaimAuthorizationRequested(AuthorizationContext context)
        {
            return context.ActionDescriptor
                .FilterDescriptors
                .Any(x => x.Filter.GetType() == typeof(AuthorizeClaimAttribute));
        }

        private bool HasUserRequestedClaim(AuthorizationContext context)
        {
            var user = context.HttpContext.User;

            return user.IsSignedIn() && user.HasClaim(Type.ToString(), Value.ToString());
        }
    }
}
