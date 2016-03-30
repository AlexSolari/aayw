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
using AAYW.Core.Models.Admin.Bussines;
using AAYW.Core.Models.Bussines.Admin;
using AAYW.Core.Models.View.UserForm;
using AAYW.Core.Web.DataBinders;
using AAYW.Core.Map;
using AAYW.Core.Extensions;
using System.Xml.Linq;
using System.Xml;
using AAYW.Core.Web.Controller.Concrete;
using AAYW.Core.Reflector;

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
            Resolver.RegisterType<IReflectionData, EntityReflectionData>();
            Resolver.RegisterType<IReflector, EntityReflector>();

            // Registrering entites
            Resolver.RegisterType<User, User>();
            Resolver.RegisterType<MailTemplate, MailTemplate>();
            Resolver.RegisterType<UserForm, UserForm>();
            Resolver.RegisterType<ContentBlock, ContentBlock>();
            Resolver.RegisterType<WebsiteSetting, WebsiteSetting>();

            // Registering providers
            Resolver.RegisterType<IProvider<User>, UserProvider>();
            Resolver.RegisterType<IProvider<UserForm>, UserFormProvider>();
            Resolver.RegisterType<IProvider<MailTemplate>, MailTemplateProvider>();
            Resolver.RegisterType<IProvider<ContentBlock>, ContentBlockProvider>();
            Resolver.RegisterType<IProvider<WebsiteSetting>, WebsiteSettingsProvider>();

            // Registering managers
            Resolver.RegisterType<IManager<User>, UserManager>();
            Resolver.RegisterType<IManager<UserForm>, UserFormManager>();
            Resolver.RegisterType<IManager<ContentBlock>, ContentBlockManager>();
            Resolver.RegisterType<IManager<MailTemplate>, MailTemplateManager>();
            Resolver.RegisterType<IManager<WebsiteSetting>, WebsiteSettingsManager>();

            //Controllers
            RegisterControllers();
            //Routing
            RegisterRoutes(RouteTable.Routes);
            //DataBinders
            ModelBinders.Binders.Add(typeof(UserFormDesignModel), new UserFormDataBinder());
            //Custom Mappings
            RegisterCustomMappings();
        }

        private static void RegisterCustomMappings()
        {
            Mapper.AddMapping<UserForm, UserFormDesignModel>((result, source) =>
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(source.FormFields.Serialize());

                result.Fields = xml;

                return result;
            });

            Mapper.AddMapping<UserFormDesignModel, UserForm>((result, source) =>
            {
                var list = source.Fields.DeserializeAs<List<UserFormField>>();

                result.FormFields = list;

                return result;
            });
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

            Map(routes, "Admin", "EntityInspector", "admin/entity/inspector/{type}/{page}", "EntityInspector");
            Map(routes, "Admin", "EditEntity", "admin/entity/edit/{type}/{id}", "EditEntity");
            Map(routes, "Admin", "SaveEntity", "admin/entity/save", "SaveEntity");

            Map(routes, "Admin", "MailSettings", "admin/mail/settings", "MailSettings");
            Map(routes, "Admin", "MailTemplates", "admin/mail/templates/{page}", "MailTemplates");
            Map(routes, "Admin", "CreateOrUpdateMailTemplate", "admin/mail/createtemplate", "CreateMailTemplate");
            Map(routes, "Admin", "UpdateMailTemplate", "admin/mail/edittemplate/{id}", "EditMailTemplate");
            Map(routes, "Admin", "DeleteMailTemplate", "admin/mail/deletetemplate/{id}", "DeleteMailTemplate");

            Map(routes, "Admin", "CreateCustomForm", "admin/forms/create", "CreateCustomForm");
            Map(routes, "Admin", "CustomFormField", "admin/forms/field/{index}", "CustomFormField");
            Map(routes, "Admin", "CustomFormsList", "admin/forms/list/{page}", "CustomFormsList");
            Map(routes, "Admin", "EditUserForm", "admin/forms/edit/{id}", "EditUserForm");
            Map(routes, "Admin", "DeleteUserForm", "admin/forms/delete/{id}", "DeleteUserForm");

            Map(routes, "Admin", "CreateContentBlock", "admin/contents/create", "CreateContentBlock");
            Map(routes, "Admin", "ContentBlocksList", "admin/contents/list/{page}", "ContentBlockList");
            Map(routes, "Admin", "EditContentBlock", "admin/contents/edit/{id}", "EditContentBlock");
            Map(routes, "Admin", "DeleteContentBlock", "admin/contents/delete/{id}", "DeleteContentBlock");
  
          
            Map(routes, "UserForm", "CustomForm", "form/{url}", "CustomForm");
            Map(routes, "UserForm", "FormSubmited", "formsuccess", "FormSubmited");
            

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
            Resolver.RegisterController<UserFormController, UserFormController>("UserForm");
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
