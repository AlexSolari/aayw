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
using System.Security.Cryptography;
using AAYW.Core.Models.Bussines.User;
using AAYW.Core.Data.Managers;
using AAYW.Core.Data.Providers;
using AAYW.Core.Models.Bussines;
using AAYW.Core.Mail;

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
            Resolver.RegisterType<ICryptoProcessor<MD5>, BaseCryptoProcessor>();
            Resolver.RegisterType<IControllerFactory, BaseControllerFactory>();
            Resolver.RegisterType<IMailProcessor, MailProcessor>();

            // Registrering entites
            Resolver.RegisterType<User, User>();
            Resolver.RegisterType<WebsiteSettings, WebsiteSettings>();

            // Registering providers
            Resolver.RegisterType<IProvider<User>, UserProvider>();
            Resolver.RegisterType<IProvider<WebsiteSettings>, WebsiteSettingsProvider>();

            // Registering managers
            Resolver.RegisterType<IManager<User>, UserManager>();
            Resolver.RegisterType<IManager<WebsiteSettings>, WebsiteSettingsManager>();

            //Controllers
            RegisterControllers();
            //Routing
            RegisterRoutes(RouteTable.Routes);
        }

        private static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            Map(routes, "Home", "Index", "home", "Home");
            Map(routes, "Home", "Landing", "landing", "Landing");
            Map(routes, "Home", "Login", "login", "Login");
            Map(routes, "Home", "Logout", "logout", "Logout");
            Map(routes, "Home", "Register", "register", "Register");

            Map(routes, "Admin", "Index", "admin", "AdminHome");
            Map(routes, "Admin", "EntityInspector", "admin/inspector/{type}/{page}", "EntityInspector");
            Map(routes, "Admin", "EditEntity", "admin/entity/edit/{type}/{id}", "EditEntity");
            Map(routes, "Admin", "SaveEntity", "admin/entity/save", "SaveEntity");
            Map(routes, "Admin", "MailSettings", "admin/settings/mail", "MailSettings");

            routes.MapRoute(
                "Default",
                "",
                new { controller = "Home", action = "Index", id = "" }
            );
            
            Map(routes, "Error", "Error404", "{*url}", "Error404");
            Map(routes, "Error", "Error403", "{*url}", "Error403");
        }

        private static void RegisterControllers()
        {
            //Factory
            ControllerBuilder.Current.SetControllerFactory(Resolver.Resolve<IControllerFactory>());

            //Controllers
            Resolver.RegisterController<HomeController, HomeController>("Home");
            Resolver.RegisterController<ErrorController, ErrorController>("Error");
            Resolver.RegisterController<AdminController, AdminController>("Admin");
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

            Resolver.RouteUrl.Add(name, "/"+url);
        }
    }
}
