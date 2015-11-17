using Microsoft.AspNet.Mvc;

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
    }
}
