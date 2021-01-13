using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Cinema_WebSite
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Login",
                url: "",
                defaults: new { controller = "Login", action = "HomeLogin", id = UrlParameter.Optional }
);


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default1",
                url: "Login/SignUp",
                defaults: new { controller = "Login", action = "signUp", id = UrlParameter.Optional }
);


            routes.MapRoute(
              name: "AdministratorHome",
              url: "Administrator/AdministratorHome/{id}",
              defaults: new { controller = "Administrator", action = "AdministratorHome", id = UrlParameter.Optional }
          );

            routes.MapRoute(
            name: "DefaultApi",
            url: "{controller}/{action}/{id}",
            defaults: new { id = UrlParameter.Optional }
        );




            routes.MapRoute(
                name: "ClientHome",
                url: "Client/ClientHome",
                defaults: new { controller = "Client", action = "ClientHome", id = UrlParameter.Optional }
);


        }
    }
}

