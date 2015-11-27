using System.Web;
using System.Web.Optimization;

namespace SIGA
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-1.11.4.js",
                        "~/Scripts/jquery.unobtrusive-ajax.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        //"~/Scripts/jquery.validate.unobtrusive.js",
                        "~/Scripts/jquery.validate*"
            ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"
            ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
               "~/Scripts/knockout-{version}.js"
            ));


            bundles.Add(new ScriptBundle("~/bundles/layout").Include(
                  "~/Scripts/SIGA/Shared/layout.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/SIGA").Include(
                "~/Scripts/SIGA/menu.js",
                "~/Scripts/SIGA/UsuarioData.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/helper.css",
                      "~/Content/site.css",
                      "~/Content/simple-sidebar.css",
                      "~/Content/font-awesome.css"
            ));
        }
    }
}
