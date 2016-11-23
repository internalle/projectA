using System.Web;
using System.Web.Optimization;

namespace ProjectA.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/client/js").Include(
                      "~/client/js/main.js"));

            bundles.Add(new ScriptBundle("~/client/vendor/js").Include(
                      "~/client/bower_components/jquery/dist/jquery.js",
                      "~/client/bower_components/jquery-ui/jquery-ui.js",
                      "~/client/bower_components/bootstrap/dist/js/bootstrap.js",
                      "~/client/bower_components/semantic/dist/semantic.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/client/css").Include(
                      "~/client/css/style.css"));

            bundles.Add(new StyleBundle("~/client/vendor/css").Include(
                      "~/client/bower_components/bootstrap/dist/css/bootstrap.css",
                      "~/client/bower_components/semantic/dist/semantic.css",
                      "~/client/bower_components/jquery-ui/themes/ui-lightness/theme.css"));
        }
    }
}
