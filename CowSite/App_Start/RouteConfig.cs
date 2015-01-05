using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CowSite
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // 繁殖部分路由  
            routes.MapRoute(
                "Breed", // 路由名称  
                "Breed/{controller}/{action}/{id}", // 带有参数的 URL  
                new { controller = "Index", action = "Index", id = UrlParameter.Optional }, // 参数默认值 
                new string[] { "CowSite.Controllers.Breed" }
            );

            // 饲养部分路由  
            routes.MapRoute(
                "Feed", // 路由名称  
                "Feed/{controller}/{action}/{id}", // 带有参数的 URL  
                new { controller = "Index", action = "Index", id = UrlParameter.Optional }, // 参数默认值  
                new string[] { "CowSite.Controllers.Feed" }
            );

            // 奶厅部分路由  
            routes.MapRoute(
                "Milk", // 路由名称  
                "Milk/{controller}/{action}/{id}", // 带有参数的 URL  
                new { controller = "Index", action = "Index", id = UrlParameter.Optional }, // 参数默认值  
                new string[] { "CowSite.Controllers.Milk" }
            );

            // 兽医部分路由  
            routes.MapRoute(
                "Medical", // 路由名称  
                "Medical/{controller}/{action}/{id}", // 带有参数的 URL  
                new { controller = "Index", action = "Index", id = UrlParameter.Optional }, // 参数默认值  
                new string[] { "CowSite.Controllers.Medical" }
            );

            // 平台部分路由  
            routes.MapRoute(
                "Platform", // 路由名称  
                "Platform/{controller}/{action}/{id}", // 带有参数的 URL  
                new { controller = "Index", action = "Index", id = UrlParameter.Optional }, // 参数默认值  
                new string[] { "CowSite.Controllers.Platform" }
            );

            // 用户管理部分路由  
            routes.MapRoute(
                "Users", // 路由名称  
                "Users/{controller}/{action}/{id}", // 带有参数的 URL  
                new { controller = "Index", action = "Index", id = UrlParameter.Optional }, // 参数默认值  
                new string[] { "CowSite.Controllers.Users" }
            );

            // 任务部分路由  
            routes.MapRoute(
                "Task", // 路由名称  
                "Task/{controller}/{action}/{id}", // 带有参数的 URL  
                new { controller = "Index", action = "Index", id = UrlParameter.Optional }, // 参数默认值  
                new string[] { "CowSite.Controllers.Task" }
            );

            // 场长部分路由  
            routes.MapRoute(
                "FarmAdmin", // 路由名称  
                "FarmAdmin/{controller}/{action}/{id}", // 带有参数的 URL  
                new { controller = "Index", action = "Index", id = UrlParameter.Optional }, // 参数默认值  
                new string[] { "CowSite.Controllers.FarmAdmin" }
            );

            // 默认路由
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            );
        }
    }
}
