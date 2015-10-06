using Autofac;
using ITLearning.Frontend.Web.Core.Identity.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Frontend.Web
{
    public class FrontendModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .Register(ctx => typeof(IIdentityService))
                .As<IdentityService>();
        }
    }
}
