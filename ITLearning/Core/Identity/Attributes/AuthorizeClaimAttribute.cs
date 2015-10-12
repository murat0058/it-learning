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
        public ClaimTypeEnum Type { get; set; }
        public ClaimValueEnum Value { get; set; }

        public override Task OnAuthorizationAsync(AuthorizationContext context)
        {
            if(IsClaimPermissionRequired(context) && HasUserRequestedClaim(context) == false)
            {
                context.Result = 
                    new RedirectToActionResult(
                        IdentityRouteValues.UnauthorizedActionRoute, 
                        IdentityRouteValues.UnauthorizedControllerRoute, 
                        new Dictionary<string, object>
                        {
                            { "returnUrl", context.HttpContext.Request.Path }
                        });
            }
            
            return Task.FromResult<object>(null);
        }

        private bool IsClaimPermissionRequired(AuthorizationContext context)
        {
            // Descriptors collection contains ordered filters of requred action. Items
            // at the end of collection are the most specific ones (probably for action).
            // So it could be i.e.: [ controller filter, action filter, action filter ]
            // but never [ action filter, controller filter ].
            var descriptors = context.ActionDescriptor.FilterDescriptors;

            var isAnonymousAccessAllowed = descriptors.Select(r => r.Filter).OfType<AllowAnonymousAttribute>().Any();
            var isClaimRequested = descriptors.Select(r => r.Filter).OfType<AuthorizeClaimAttribute>().Any();

            if(!isAnonymousAccessAllowed && !isClaimRequested)
            {
                throw new ArgumentException($"Filter descriptors for action {context.ActionDescriptor.Name} do not contain either AllowAnonymousAttribute, or AuthorizeClaimAttribute.");
            }

            return !isAnonymousAccessAllowed;
        }

        private bool HasUserRequestedClaim(AuthorizationContext context)
        {
            var user = context.HttpContext.User;

            return user.IsSignedIn() && user.HasClaim(Type.ToString(), Value.ToString());
        }
    }
}
