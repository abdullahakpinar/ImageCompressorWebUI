using System.Web.Optimization;

namespace ImageCompressorUI
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Content/js").Include(
                        "~/Content/js/jquery-3.4.1.min.js", "~/Content/js/bootstrap.min.js", "~/Content/js/popper.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/bootstrap.min.css"));

        }
    }
}
