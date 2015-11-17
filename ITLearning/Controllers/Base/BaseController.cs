using ITLearning.Frontend.Web.Common;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Filters;

namespace ITLearning.Frontend.Web.Controllers
{
    [Route("Base")]
    public abstract class BaseController : Controller
    {
        [Route("IsAuthorized")]
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
