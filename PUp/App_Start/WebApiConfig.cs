using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace PUp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuration et services API Web
             var cors = new System.Web.Http.Cors.EnableCorsAttribute("*", "*", "*");
             config.EnableCors(cors);
            // Itinéraires de l'API Web
            config.MapHttpAttributeRoutes();
            // config.EnableCors //enable CORS for ALL!
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
