using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("PreReg", "prereg",
                defaults: new { Controller = "Interested", action = "Express", id = UrlParameter.Optional }
            );

            routes.MapRoute("AssignNumber", "assignnumber",
                defaults: new { Controller = "Interested", action = "AssignNumber", id = UrlParameter.Optional }
            );

            routes.MapRoute("Assess", "assess",
                defaults: new { Controller = "Skill", action = "TrialAssessment", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Interested",
                url: "Player/Interested/{action}/{id}",
                defaults: new { controller = "Interested", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Skill",
                url: "Player/Skill/{action}/{id}",
                defaults: new { controller = "Skill", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
