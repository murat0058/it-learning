using ITLearning.Shared;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Filters;

namespace ITLearning.Frontend.Web.Controllers.Base
{
    [Route("Base")]
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
