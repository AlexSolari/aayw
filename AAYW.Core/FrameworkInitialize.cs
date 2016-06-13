﻿using AAYW.Core.Controller.Concrete;
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

            Map(routes, "Admin", "EntityInspector", "admin/entity/inspector/{type}/{page}", "EntityInspector");
            Map(routes, "Admin", "EditEntity", "admin/entity/edit/{type}/{id}", "EditEntity");
            Map(routes, "Admin", "SaveEntity", "admin/entity/save", "SaveEntity");
            Map(routes, "Admin", "DeleteEntity", "admin/entity/delete", "DeleteEntity");

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

            Map(routes, "Admin", "CreatePage", "admin/pages/create", "CreatePage");
            Map(routes, "Admin", "PagesList", "admin/pages/list/{page}", "PageList");
            Map(routes, "Admin", "EditPage", "admin/pages/edit/{id}", "EditPage");
            Map(routes, "Admin", "DeletePage", "admin/pages/delete/{id}", "DeletePage");

            Map(routes, "Admin", "Log", "admin/log/{count}", "Log");
            Map(routes, "Admin", "EntireLog", "admin/fulllog", "EntireLog");

            Map(routes, "Admin", "Cache", "admin/cache", "Cache");
            Map(routes, "Admin", "DropCache", "admin/cache/drop", "DropCache");

            Map(routes, "Admin", "ChangeLogo", "admin/general/logo", "ChangeLogo");
            Map(routes, "Admin", "ChangeColorTheme", "admin/general/theme", "ChangeColorTheme");
            Map(routes, "Admin", "ResetColorTheme", "admin/general/themereset", "ResetColorTheme");

          
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
            Resolver.RegisterController<AdminController, AdminController>("Admin");
            Resolver.RegisterController<UserFormController, UserFormController>("UserForm");
            Resolver.RegisterController<PageController, PageController>("Pages");
            Resolver.RegisterController<FeedController, FeedController>("Feed");
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
