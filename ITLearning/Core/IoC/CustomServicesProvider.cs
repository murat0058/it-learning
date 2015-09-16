using ITLearning.Frontend.Web.Core.Identity.Services;
using Microsoft.Framework.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Frontend.Web.Core.IoC
{
    public static class CustomServicesProvider
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //Instance – a specific instance is given all the time.You are responsible for its initial creation.
            //Transient – a new instance is created every time.
            //Singleton – a single instance is created and it acts like a singleton.
            //Scoped – a single instance is created inside the current scope.It is equivalent to Singleton in the current scope

            services.AddTransient<IIdentityService, IdentityService>();
        }
    }
}
