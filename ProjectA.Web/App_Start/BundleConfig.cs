using ProjectA.Configuration.Base;
using System.Web;
using System.Web.Optimization;

namespace ProjectA.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles, IAppSettings settings)
        {
            #region vendor libs

            bundles.Add(new ScriptBundle("~/client/vendor/js").Include(
                      "~/client/bower_components/jquery/dist/jquery.js",
                      "~/client/bower_components/jquery-ui/jquery-ui.js",
                      "~/client/bower_components/jquery-validation/dist/jquery.validate.js",
                      "~/client/bower_components/jquery-validation-unobrusive/jquery.validate.unobrusive.js",
                      "~/client/bower_components/bootstrap/dist/js/bootstrap.js",
                      "~/client/bower_components/semantic/dist/semantic.js",
                      "~/client/bower_components/toastr/toastr.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/client/vendor/css").Include(
                      "~/client/bower_components/bootstrap/dist/css/bootstrap.css",
                      "~/client/bower_components/semantic/dist/semantic.css",
                      "~/client/bower_components/jquery-ui/themes/ui-lightness/theme.css",
                      "~/client/bower_components/toastr/toastr.css"));

            #endregion

            #region our libs

            bundles.Add(new ScriptBundle("~/client/js").Include(
                      "~/client/dist/js/compiled-ts.js"));

            bundles.Add(new StyleBundle("~/client/css").Include(
                      "~/client/css/style.css"));

            #endregion

            #region should minify or not

            if (settings.IsDebug)
            {
                BundleTable.EnableOptimizations = false;
            }
            else
            {
                BundleTable.EnableOptimizations = true;
            }

            #endregion
        }
    }
}
