using AAYW.Core.Controller.Concrete;
using AAYW.Core.Crypto;
using AAYW.Core.Extensions;
using AAYW.Core.Dependecies;
using AAYW.Core.Web.ControllerFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace AAYW.Core
{
    public static class Framework
    {
        public static void Initialize()
        {
            // Register types here, and call this in Global.asax
            // Example:
            //  Resolver.RegisterType<TBase, TDerived>();
            //  Resolver.RegisterType<IInterface, TRealisation>();
            Resolver.RegisterType<ICryptoProcessor, BaseCryptoProcessor>();
            Resolver.RegisterType<IControllerFactory, BaseControllerFactory>();

            //Controllers
            RegisterControllers();
            //Routing
            RegisterRoutes(RouteTable.Routes);
        }

        private static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            Map(routes, "Home", "Index", "", "Home");
            Map(routes, "Home", "Landing", "landing", "Landing");
            Map(routes, "Home", "Login", "login", "Login");
            Map(routes, "Home", "Logout", "logout", "Logout");
            Map(routes, "Home", "Register", "register", "Register");
            routes.MapRoute("Default", "{controller}/{action}", new { controller = "Home", action = "Index" });
        }

        private static void RegisterControllers()
        {
            //Factory
            ControllerBuilder.Current.SetControllerFactory(Resolver.Resolve<IControllerFactory>());

            //Controllers
            Resolver.RegisterController<HomeController, HomeController>("Home");
        }

        private static void Map(RouteCollection routes, string controller, string action, string url = null, string name = null)
        {

            if (name.IsNullOrWhiteSpace())
            {
                name = "{0}-{1}".FormatWith(controller, action);
            }

            if (url.IsNullOrWhiteSpace())
            {
                url = "{0}/{1}".FormatWith(controller, action);
            }

            routes.MapRoute(
                name: name,
                url: url,
                defaults: new { controller = controller, action = action }
            );
        }
    }
}
