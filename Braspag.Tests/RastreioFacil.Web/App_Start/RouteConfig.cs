using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RastreioFacil.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Boleto",
                url: "Boleto/{hash}",
                defaults: new { controller = "Default", action = "GerarBoleto", hash = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Monitoramento",
                url: "Monitoramento",
                defaults: new { controller = "Monitoramento", action = "index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Default", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
