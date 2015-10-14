using Microsoft.AspNet.Mvc;

namespace ITLearning.Frontend.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        public virtual bool IsAuthorized()
        {
            return User.Identity.IsAuthenticated;
        }
    }
}
