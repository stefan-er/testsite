using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ElipseshopMVC.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "TopCategoryPage",
                url: "Category/{topCategory}",
                defaults: new { controller = "Category", action = "TopCategory", topCategory = "Men" }
            );

            routes.MapRoute(
                name: "SubCategoryPage",
                url: "Category/{topCategory}/{subCategory}",
                defaults: new { controller = "Category", action = "SubCategory", topCategory = "Men", subCategory = "T-Shirts" }
            );

            routes.MapRoute(
                name: "ProductAjaxRequests",
                url: "Product/{action}",
                defaults: new { controller = "Product", action = "Product" }
            );

            routes.MapRoute(
                name: "ProductPage",
                url: "Product/{productID}/{productTitle}",
                defaults: new { controller = "Product", action = "Product", productID = "1", productTitle = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "StaticPage",
                url: "{action}",
                defaults: new { controller = "StaticPages" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "StaticPages", action = "Home", id = UrlParameter.Optional }
            );
        }
    }
}
