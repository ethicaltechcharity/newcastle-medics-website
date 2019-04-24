using Ninject;
using Ninject.Web.Common.WebHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Web.Helpers;
using Web.Helpers.Classes;
using Web.Helpers.Interfaces;

namespace Web
{
    public class MvcApplication : NinjectHttpApplication
    {
        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            kernel.Bind<IUserHelper>().To<UserHelper>();
            kernel.Bind<IPlayerHelper>().To<PlayerHelper>();
            kernel.Bind<IMemberHelper>().To<MemberHelper>();
            kernel.Bind<ITeamHelper>().To<TeamHelper>();
            kernel.Bind<ISkillHelper>().To<SkillHelper>();
            kernel.Bind<ICaptchaHelper>().To<ReCaptureV2Helper>();
            return kernel;
        }
    }
}
