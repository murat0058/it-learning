using Microsoft.AspNet.Html.Abstractions;
using Microsoft.AspNet.Mvc.Rendering;
using ITLearning.Contract.Enums;
using System;
using System.Security.Claims;

namespace ITLearning.Frontend.Web.Common.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlContent ToJsString(this IHtmlHelper helper, string str)
        {
            return helper.Raw($"\"{str}\"");
        }

        public static IHtmlContent PartialWithClaim(this IHtmlHelper helper, string partialName, ClaimsPrincipal user, ClaimTypeEnum claimType, ClaimValueEnum claimValue)
        {
            if (user.HasClaim(claimType.ToString(), claimValue.ToString()))
            {
                return helper.Partial(partialName);
            }
            else
            {
                return helper.Raw(string.Empty);
            }
            
        }

        public static IHtmlContent PartialWithClaim(this IHtmlHelper helper, string partialName, object model, ClaimsPrincipal user, ClaimTypeEnum claimType, ClaimValueEnum claimValue)
        {
            if (user.HasClaim(claimType.ToString(), claimValue.ToString()))
            {
                return helper.Partial(partialName, model: model);
            }
            else
            {
                return helper.Raw(string.Empty);
            }
        }

        public static IHtmlContent PartialWithRole(this IHtmlHelper helper, string partialName, ClaimsPrincipal user, UserRoleEnum userRole)
        {
            if (user.IsInRole(userRole.ToString()))
            {
                return helper.Partial(partialName);
            }
            else
            {
                return helper.Raw(string.Empty);
            }
        }

        public static IHtmlContent PartialWithRole(this IHtmlHelper helper, string partialName, object model, ClaimsPrincipal user, UserRoleEnum userRole)
        {
            if (user.IsInRole(userRole.ToString()))
            {
                return helper.Partial(partialName, model: model);
            }
            else
            {
                return helper.Raw(string.Empty);
            }
        }
    }
}
