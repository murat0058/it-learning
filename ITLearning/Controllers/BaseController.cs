using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Frontend.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        public virtual bool Authorized()
        {
            return User.Identity.IsAuthenticated;
        }
    }
}
