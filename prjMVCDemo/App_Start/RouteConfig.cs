using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace prjMVCDemo
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

         //   routes.MapRoute(
         //name: "Customer",
         //url: "{action}/{controller}/{id}",
         //defaults: new { controller = "Customer", action = "List", id = UrlParameter.Optional });


            routes.MapRoute(
                name: "Default", //名稱
                url: "{controller}/{action}/{id}", //規則
                defaults: new { controller = "Common", action = "Login", id = UrlParameter.Optional } //預設值
            );
        }
    }
}
