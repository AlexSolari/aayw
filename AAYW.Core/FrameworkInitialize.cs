using AAYW.Core.Controller.Concrete;
using AAYW.Core.Crypto;
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
using System.Xml.Linq;
using System.Xml;
using AAYW.Core.Web.Controller.Concrete;
using AAYW.Core.Reflector;
using AAYW.Core.Models.View.Page;
using AAYW.Core.Models.Bussines.Post;
using AAYW.Core.Logging;
using AAYW.Core.Cache;
using AAYW.Core.Api;
using AAYW.Core.Controller.Concrete.Admin;

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
            Resolver.RegisterType<ILogger, Logger>();
            Resolver.RegisterType<ICache, DefaultCache>();

            // Registrering entites
            Resolver.RegisterType<Post, Post>(true);
            Resolver.RegisterType<User, User>(true);
            Resolver.RegisterType<Page, Page>(true);
            Resolver.RegisterType<UserForm, UserForm>(true);
            Resolver.RegisterType<LogMessage, LogMessage>(true);
            Resolver.RegisterType<PostComment, PostComment>(true);
            Resolver.RegisterType<MailTemplate, MailTemplate>(true);
            Resolver.RegisterType<ContentBlock, ContentBlock>(true);
            Resolver.RegisterType<WebsiteSetting, WebsiteSetting>(true);

            // Registering providers
            Resolver.RegisterType<IProvider<Post>, PostProvider>();
            Resolver.RegisterType<IProvider<User>, UserProvider>();
            Resolver.RegisterType<IProvider<Page>, PageProvider>();
            Resolver.RegisterType<IProvider<UserForm>, UserFormProvider>();
            Resolver.RegisterType<IProvider<PostComment>, PostCommentProvider>();
            Resolver.RegisterType<IProvider<LogMessage>, LogMessageProvider>();
            Resolver.RegisterType<IProvider<MailTemplate>, MailTemplateProvider>();
            Resolver.RegisterType<IProvider<ContentBlock>, ContentBlockProvider>();
            Resolver.RegisterType<IProvider<WebsiteSetting>, WebsiteSettingsProvider>();

            // Registering managers
            Resolver.RegisterType<IManager<Post>, PostManager>();
            Resolver.RegisterType<IManager<User>, UserManager>();
            Resolver.RegisterType<IManager<Page>, PageManager>();
            Resolver.RegisterType<IManager<LogMessage>, LogManager>();
            Resolver.RegisterType<IManager<UserForm>, UserFormManager>();
            Resolver.RegisterType<IManager<PostComment>, PostCommentManager>();
            Resolver.RegisterType<IManager<ContentBlock>, ContentBlockManager>();
            Resolver.RegisterType<IManager<MailTemplate>, MailTemplateManager>();
            Resolver.RegisterType<IManager<WebsiteSetting>, WebsiteSettingsManager>();

            var logger = SiteApi.Services.Logger;

            //Controllers
            logger.Log("Registering controllers and controller factory");
            RegisterControllers();
            //Routing
            logger.Log("Registering routes");
            RegisterRoutes(RouteTable.Routes);
            //DataBinders
            logger.Log("Registering model binders");
            ModelBinders.Binders.Add(typeof(UserFormDesignModel), new UserFormDataBinder());
            //Custom Mappings
            logger.Log("Registering custom mappings");
            RegisterCustomMappings();

            logger.Log("AAYW Framework initialized");
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

            Mapper.AddMapping<Page, PageDesignModel>((result, source) =>
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(source.ContentBlocks.Serialize());

                result.ContentBlocks = xml;

                return result;
            });

            Mapper.AddMapping<PageDesignModel, Page>((result, source) =>
            {
                var list = source.ContentBlocks.DeserializeAs<List<string>>();

                result.ContentBlocks = list;

                return result;
            });
        }

        private static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            Map(routes, "Home", "Index", "", "Home");
            Map(routes, "Home", "Theme", "css", "Theme");
            Map(routes, "Home", "Landing", "landing", "Landing");
            Map(routes, "Home", "Login", "login", "Login");
            Map(routes, "Home", "Logout", "logout", "Logout");
            Map(routes, "Home", "Register", "register", "Register");


            Map(routes, "Admin", "Index", "admin", "AdminHome");

            Map(routes, "AdminEntities", "EntityInspector", "admin/entity/inspector/{type}/{page}", "EntityInspector");
            Map(routes, "AdminEntities", "EditEntity", "admin/entity/edit/{type}/{id}", "EditEntity");
            Map(routes, "AdminEntities", "SaveEntity", "admin/entity/save", "SaveEntity");
            Map(routes, "AdminEntities", "DeleteEntity", "admin/entity/delete", "DeleteEntity");

            Map(routes, "AdminMailSettings", "MailSettings", "admin/mail/settings", "MailSettings");

            Map(routes, "AdminMailTemplates", "MailTemplates", "admin/mail/templates/{page}", "MailTemplates");
            Map(routes, "AdminMailTemplates", "CreateOrUpdateMailTemplate", "admin/mail/createtemplate", "CreateMailTemplate");
            Map(routes, "AdminMailTemplates", "UpdateMailTemplate", "admin/mail/edittemplate/{id}", "EditMailTemplate");
            Map(routes, "AdminMailTemplates", "DeleteMailTemplate", "admin/mail/deletetemplate/{id}", "DeleteMailTemplate");

            Map(routes, "AdminCustomForms", "CreateCustomForm", "admin/forms/create", "CreateCustomForm");
            Map(routes, "AdminCustomForms", "CustomFormField", "admin/forms/field/{index}", "CustomFormField");
            Map(routes, "AdminCustomForms", "CustomFormsList", "admin/forms/list/{page}", "CustomFormsList");
            Map(routes, "AdminCustomForms", "EditUserForm", "admin/forms/edit/{id}", "EditUserForm");
            Map(routes, "AdminCustomForms", "DeleteUserForm", "admin/forms/delete/{id}", "DeleteUserForm");

            Map(routes, "AdminContentBlocks", "CreateContentBlock", "admin/contents/create", "CreateContentBlock");
            Map(routes, "AdminContentBlocks", "ContentBlocksList", "admin/contents/list/{page}", "ContentBlockList");
            Map(routes, "AdminContentBlocks", "EditContentBlock", "admin/contents/edit/{id}", "EditContentBlock");
            Map(routes, "AdminContentBlocks", "DeleteContentBlock", "admin/contents/delete/{id}", "DeleteContentBlock");

            Map(routes, "AdminPages", "CreatePage", "admin/pages/create", "CreatePage");
            Map(routes, "AdminPages", "PagesList", "admin/pages/list/{page}", "PageList");
            Map(routes, "AdminPages", "EditPage", "admin/pages/edit/{id}", "EditPage");
            Map(routes, "AdminPages", "DeletePage", "admin/pages/delete/{id}", "DeletePage");

            Map(routes, "AdminLogging", "Log", "admin/log/{count}", "Log");
            Map(routes, "AdminLogging", "EntireLog", "admin/fulllog", "EntireLog");

            Map(routes, "AdminCaching", "Cache", "admin/cache", "Cache");
            Map(routes, "AdminCaching", "DropCache", "admin/cache/drop", "DropCache");

            Map(routes, "AdminGeneral", "ChangeLogo", "admin/general/logo", "ChangeLogo");
            Map(routes, "AdminGeneral", "ChangeColorTheme", "admin/general/theme", "ChangeColorTheme");
            Map(routes, "AdminGeneral", "ResetColorTheme", "admin/general/themereset", "ResetColorTheme");

          
            Map(routes, "UserForm", "CustomForm", "form/{url}", "CustomForm");
            Map(routes, "UserForm", "FormSubmited", "formsuccess", "FormSubmited");

            Map(routes, "Feed", "CreatePost", "post/new/{feedId}", "CreatePost");
            Map(routes, "Feed", "EditPost", "post/edit/{postId}", "EditPost");
            Map(routes, "Feed", "CreateOrUpdatePost", "post/save", "CreateOrUpdatePost");
            Map(routes, "Feed", "DeletePost", "post/delete/{id}", "DeletePost");

            Map(routes, "Pages", "Post", "post/{id}", "Post");
            Map(routes, "Pages", "AddPostComment", "addcomment", "AddPostComment");

            routes.MapRoute(
                "Default",
                "",
                new { controller = "Home", action = "Index", id = "" }
            );

            Map(routes, "Pages", "Page", "{*url}", "CustomPage");
            
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
            Resolver.RegisterController<UserFormController, UserFormController>("UserForm");
            Resolver.RegisterController<PageController, PageController>("Pages");
            Resolver.RegisterController<FeedController, FeedController>("Feed");
         
            Resolver.RegisterController<AdminBaseController, AdminBaseController>("Admin");
            Resolver.RegisterController<GeneralController, GeneralController>("AdminGeneral");
            Resolver.RegisterController<CachingController, CachingController>("AdminCaching");
            Resolver.RegisterController<LoggingController, LoggingController>("AdminLogging");
            Resolver.RegisterController<PagesController, PagesController>("AdminPages");
            Resolver.RegisterController<ContentBlocksController, ContentBlocksController>("AdminContentBlocks");
            Resolver.RegisterController<CustomFormsController, CustomFormsController>("AdminCustomForms");
            Resolver.RegisterController<MailTemplatesController, MailTemplatesController>("AdminMailTemplates");
            Resolver.RegisterController<MailSettingsController, MailSettingsController>("AdminMailSettings");
            Resolver.RegisterController<EntitiesController, EntitiesController>("AdminEntities");
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
