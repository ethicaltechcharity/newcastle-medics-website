using System.Web;
using System.Web.Optimization;

namespace Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap/bootstrap.js",
                "~/Scripts/bootstrap/bootstrap-select.js"));

            bundles.Add(new ScriptBundle("~/bundles/popper").Include(
                "~/Scripts/umd/popper.js",
                "~/Scripts/umd/popper-utils.js"));

            bundles.Add(new ScriptBundle("~/bundles/select2").Include(
                "~/Scripts/select2/select2.js"));

            bundles.Add(new ScriptBundle("~/bundles/site/formation").Include(
                "~/Scripts/Site/formation.js"));

            bundles.Add(new ScriptBundle("~/bundles/pages/assign-number").Include(
                "~/Scripts/Pages/assign-number.js"));

            bundles.Add(new ScriptBundle("~/bundles/pages/user").Include(
                "~/Scripts/Pages/User/user.js"));

            bundles.Add(new ScriptBundle("~/bundles/pages/member").Include(
                "~/Scripts/Pages/Member/member.js"));

            bundles.Add(new ScriptBundle("~/bundles/pages/member/register").Include(
                "~/Scripts/Pages/Member/register.js"));

            bundles.Add(new ScriptBundle("~/bundles/pages/player/skill").Include(
                "~/Scripts/Pages/Player/Skill/skill.js"));

            bundles.Add(new ScriptBundle("~/bundles/pages/player/skill/match-assessment").Include(
                "~/Scripts/Pages/Player/Skill/match-assessment.js"));

            bundles.Add(new StyleBundle("~/CSS").Include(
                "~/Content/CSS/bootstrap/bootstrap.css",
                "~/Content/CSS/bootstrap/bootstrap-select.css",
                "~/Content/CSS/bootstrap/bootstrap-grid.css",
                "~/Content/CSS/bootstrap/bootstrap-reboot.css",
                "~/Content/CSS/select2/select2.css",
                "~/Content/CSS/site.css"));
        }
    }
}
