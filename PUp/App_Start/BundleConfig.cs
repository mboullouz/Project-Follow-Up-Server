using Quartz;
using Quartz.Impl;
using System.Web;
using System.Web.Optimization;

namespace PUp
{  
    
    public class BundleConfig
    {
        // Pour plus d'informations sur le regroupement, visitez http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/zsLib/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/zsLib/jquery.validate*"));

            // Utilisez la version de développement de Modernizr pour le développement et l'apprentissage. Puis, une fois
            // prêt pour la production, utilisez l'outil de génération (bluid) sur http://modernizr.com pour choisir uniquement les tests dont vous avez besoin.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/zsLib/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/zsLib/bootstrap.js",
                      "~/Scripts/zsLib/respond.js"));

            bundles.Add(new StyleBundle("~/Scripts/style/Content/css").Include(
                      "~/Scripts/style/Content/bootstrap.css",
                      "~/Scripts/style/Content/font-awesome.css",
                      "~/Scripts/style/Content/site.css",
                      "~/Scripts/style/Content/notif.css",
                      "~/Scripts/style/Content/timeline.css"
                      ));

           
        }
    }
}
