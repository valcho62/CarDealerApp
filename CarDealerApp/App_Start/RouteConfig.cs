using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CarDealerApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();


            routes.MapRoute(
          name: "Sales by Id",
          url: "Sales/{id}",
          defaults: new { controller = "Sales", action = "Details"}
      );
            
            routes.MapRoute(
             name: "Cars with parts",
             url: "Cars/{id}/parts",
             defaults: new { controller = "Cars", action = "Parts" }
         );
            routes.MapRoute(
              name: "Suppliers",
              url: "supliers/{id}",
              defaults: new { controller = "Supliers", action = "Filter" }
          );
            routes.MapRoute(
              name: "Cars",
              url: "Cars/{id}",
              defaults: new { controller = "Cars", action = "Index", id = UrlParameter.Optional }
          );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );



        }
    }
}
