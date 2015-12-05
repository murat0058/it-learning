using Microsoft.AspNet.Html.Abstractions;
using Microsoft.AspNet.Mvc.Rendering;

namespace ITLearning.Frontend.Web.Common.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlContent ToJsString(this IHtmlHelper helper, string str)
        {
            return helper.Raw($"\"{str}\"");
        }
    }
}
