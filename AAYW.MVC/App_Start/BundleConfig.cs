using System.Web;
using System.Web.Optimization;

namespace AAYW.MVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                        "~/Scripts/lib/AAYW*"));

            bundles.Add(new ScriptBundle("~/bundles/nicedit").Include(
                        "~/Scripts/nicedit/nicEdit.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/Scripts").Include(
                        "~/Scripts/main.js", 
                        "~/Scripts/mui.min.js"));

            bundles.Add(new ScriptBundle("~/Scripts/Admin").Include(
                        "~/Scripts/admin.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/mui").Include(
                      "~/Content/css/mui.css"));
        }
    }
}
