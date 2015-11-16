using ITLearning.Frontend.Web.Common;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Filters;

namespace ITLearning.Frontend.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        public virtual bool IsAuthorized()
        {
            return User.Identity.IsAuthenticated;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (IsAuthorized())
            {
                StaticManager.UserName = User.Identity.Name;
            }

            base.OnActionExecuting(context);
        }
    }
}