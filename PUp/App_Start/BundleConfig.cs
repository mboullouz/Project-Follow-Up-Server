using Quartz;
using Quartz.Impl;
using System.Web;
using System.Web.Optimization;

namespace PUp
{

    public class BundleConfig
    {

        public static void RegisterBundles(BundleCollection bundles)
        {


            /*bundles.Add(new ScriptBundle("~/bundles/libs").IncludeDirectory(
                        "~/Scripts/zsLib","*.js",true));

            bundles.Add(new ScriptBundle("~/bundles/pages").Include(
                        "~/Scripts/src/theme/pages/*.js"));

            bundles.Add(new ScriptBundle("~/bundles/theme/js").Include(
                        "~/Scripts/js/*.js"));

            */



            //Styles
            bundles.Add(new StyleBundle("~/Scripts/style/Content/css").Include(
                      "~/Scripts/css/*.css"
                      ));


        }
    }
}
