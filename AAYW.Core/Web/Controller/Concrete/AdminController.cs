using AAYW.Core.Data.Managers;
using AAYW.Core.Dependecies;
using System.Web.Mvc;
using AAYW.Core.Models.Bussines;
using AAYW.Resources;
using System.Linq;
using System;
using AAYW.Core.Models.View;
using AAYW.Core.Web.Controller;
using System.Web.Routing;
using AAYW.Core.Models.View.User;
using AAYW.Core.Annotations;
using AAYW.Core.Models.Bussines.User;
using AAYW.Core.Extensions;
using System.Reflection;
using System.Collections.Generic;
using AAYW.Core.Map;
using AAYW.Core.Models.View.Settings;
using AAYW.Core.Models.Admin.Bussines;
using AAYW.Core.Models.Bussines.Admin;
using AAYW.Core.Models.View.MailTemplates;
using AAYW.Core.Models.View.UserForm;
using AAYW.Core.Reflector;
using AAYW.Core.Models.View.ContentBlock;
using AAYW.Core.Models.View.Page;
using AAYW.Core.Logging;
using AAYW.Core.Cache;

namespace AAYW.Core.Controller.Concrete
{
    [AccessLevel(Models.Bussines.User.User.Role.Admin)]
    public class AdminController : FrontendController
    {
        WebsiteSettingsManager settingsManager = (WebsiteSettingsManager)AAYW.Core.Dependecies.Resolver.GetInstance<IManager<WebsiteSetting>>();
        IManager<MailTemplate> mailTemplatesManager = AAYW.Core.Dependecies.Resolver.GetInstance<IManager<MailTemplate>>();
        IManager<UserForm> userFormsManager = AAYW.Core.Dependecies.Resolver.GetInstance<IManager<UserForm>>();
        IManager<ContentBlock> contentBlocksManager = AAYW.Core.Dependecies.Resolver.GetInstance<IManager<ContentBlock>>();
        IManager<Page> pagesManager = AAYW.Core.Dependecies.Resolver.GetInstance<IManager<Page>>();

        ILogger logger = AAYW.Core.Dependecies.Resolver.GetInstance<ILogger>();

        public AdminController()
        {

        }

        public override string Name 
        { 
            get {
                return "Admin";
            }
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        #region Chaching
        [HttpGet]
        public ActionResult Cache()
        {
            var cache = AAYW.Core.Dependecies.Resolver.GetInstance<ICache>();
            return View(cache.GetAll());
        }

        [HttpGet]
        public ActionResult DropCache()
        {
            AAYW.Core.Dependecies.Resolver.GetInstance<ICache>().DropAll();
            return RedirectToAction("Cache");
        }
        #endregion
        #region Logging

        [HttpGet]
        public ActionResult EntireLog()
        {
            return View("Log", logger.GetLog());
        }

        [HttpGet]
        public ActionResult Log(int count)
        {
            return View(logger.GetLog(count));
        }
        
        #endregion
        #region Pages


        [HttpGet]
        public ActionResult PagesList(int page)
        {
            var list = pagesManager.GetList(page);
            ViewData["Page"] = page;
            return View(list);
        }

        [HttpGet]
        public ActionResult CreatePage()
        {
            var model = Dependecies.Resolver.GetInstance<PageDesignModel>(Guid.NewGuid());
            return PartialView(model);
        }

        [HttpGet]
        public ActionResult EditPage(string id)
        {
            var model = pagesManager.GetById(id);

            var mapped = Mapper.Map<PageDesignModel, Page>(model);

            return PartialView("CreatePage", mapped);
        }

        [HttpGet]
        public ActionResult DeletePage(string id)
        {
            var model = pagesManager.GetById(id);
            pagesManager.Delete(model);
            return RedirectToRoute("PageList", new { page = 0 });
        }

        [HttpPost]
        public ActionResult CreatePage(PageDesignModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(model);
            }

            var mapped = Mapper.Map<Page, PageDesignModel>(model);

            pagesManager.CreateOrUpdate(mapped);

            return Json(true);
        }
        #endregion
        #region ContentBlocks


        [HttpGet]
        public ActionResult ContentBlocksList(int page)
        {
            var list = contentBlocksManager.GetList(page);
            ViewData["Page"] = page;
            return View(list);
        }

        [HttpGet]
        public ActionResult CreateContentBlock()
        {
            var model = Dependecies.Resolver.GetInstance<ContentBlockDesignModel>(Guid.NewGuid());
            return PartialView(model);
        }

        [HttpGet]
        public ActionResult EditContentBlock(string id)
        {
            var model = contentBlocksManager.GetById(id);

            var mapped = Mapper.Map<ContentBlockDesignModel, ContentBlock>(model);

            return PartialView("CreateContentBlock", mapped);
        }

        [HttpGet]
        public ActionResult DeleteContentBlock(string id)
        {
            var model = contentBlocksManager.GetById(id);
            contentBlocksManager.Delete(model);
            return RedirectToRoute("ContentBlockList", new { page = 0 });
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult CreateContentBlock(ContentBlockDesignModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(model);
            }

            var mapped = Mapper.Map<ContentBlock, ContentBlockDesignModel>(model);

            contentBlocksManager.CreateOrUpdate(mapped);

            return Json(true);
        }
        #endregion
        #region CustomForm
        

        [HttpGet]
        public ActionResult CustomFormsList(int page)
        {
            var list = userFormsManager.GetList(page);
            ViewData["Page"] = page;
            return View(list);
        }

        [HttpGet]
        public ActionResult CreateCustomForm()
        {
            var model = Dependecies.Resolver.GetInstance<UserFormDesignModel>(Guid.NewGuid());
            return PartialView(model);
        }

        [HttpGet]
        public ActionResult EditUserForm(string id)
        {
            var model = userFormsManager.GetById(id);

            var mapped = Mapper.Map<UserFormDesignModel, UserForm>(model);

            return PartialView("CreateCustomForm", mapped);
        }

        [HttpPost]
        public ActionResult EditUserForm(UserFormDesignModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var mapped = Mapper.Map<UserForm, UserFormDesignModel>(model);

            userFormsManager.CreateOrUpdate(mapped);

            return Json(true);
        }

        [HttpGet]
        public ActionResult DeleteUserForm(string id)
        {
            var model = userFormsManager.GetById(id);
            userFormsManager.Delete(model);
            return RedirectToRoute("CustomFormsList", new { page = 0 });
        }

        [HttpPost]
        public ActionResult CreateCustomForm(UserFormDesignModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(model);
            }

            var mapped = Mapper.Map<UserForm, UserFormDesignModel>(model);

            if (((UserFormManager)userFormsManager).IsAvalibleForCreation(mapped))
            {
                userFormsManager.CreateOrUpdate(mapped);
            }
            else
            {
                ModelState.AddModelError("Url", ResourceAccessor.Instance.Get("Error_UrlMustBeUniue"));
                return PartialView(model);
            }

            return Json(true);
        }
        [HttpPost]
        public ActionResult CustomFormField(int index)
        {
            var model = Dependecies.Resolver.GetInstance<UserFormField>();
            ViewData["Index"] = index;
            return PartialView("_CustomFormField", model);
        }
        #endregion
        #region MailTemplates
        
        [HttpGet]
        public ActionResult MailTemplates(int page)
        {
            ViewData["Page"] = page;
            var templates = mailTemplatesManager.GetList(page);
            return View(templates);
        }

        [HttpGet]
        public ActionResult CreateOrUpdateMailTemplate()
        {
            return PartialView("_MailTemplateCreate", Dependecies.Resolver.GetInstance<MailTemplateCreateModel>());
        }

        [HttpGet]
        public ActionResult UpdateMailTemplate(string id)
        {
            var template = Mapper.Map<MailTemplateCreateModel, MailTemplate>(mailTemplatesManager.GetById(id));
            return PartialView("_MailTemplateCreate", template);
        }

        [HttpPost]
        public ActionResult CreateOrUpdateMailTemplate(MailTemplateCreateModel model)
        {
            var template = Mapper.Map<MailTemplate, MailTemplateCreateModel>(model);

            if (!((MailTemplateManager)mailTemplatesManager).CanCreate(template))
            {
                ModelState.AddModelError("Name", ResourceAccessor.Instance.Get("Error_TemplateExist"));
            }

            if (!ModelState.IsValid)
            {
                return PartialView("_MailTemplateCreate", model);
            }

            mailTemplatesManager.CreateOrUpdate(template);

            return Json(false);
        }

        public ActionResult DeleteMailTemplate(string id)
        {
            var model = mailTemplatesManager.GetById(id);
            mailTemplatesManager.Delete(model);
            return RedirectToRoute("MailTemplates", new { page = 0 });
        }

        #endregion
        #region MailSettins
        
        [HttpGet]
        public ActionResult MailSettings()
        {
            var websiteSettings = settingsManager.GetSettings();
            var mailSettings = Mapper.Map<MailSettings, WebsiteSetting>(websiteSettings);
            return View(mailSettings);
        }

        [HttpPost]
        public ActionResult MailSettings(MailSettings model)
        {
            var websiteSettings = settingsManager.GetSettings();
            websiteSettings = Mapper.MapAndMerge<WebsiteSetting, MailSettings>(model, websiteSettings);
            settingsManager.UpdateSettings(websiteSettings);
            return View(model);
        }
        #endregion
        #region EntityInspector
        
        [HttpGet]
        public ActionResult EntityInspector(string type, int page)
        {
            var manager = GetManager(type);
            ViewData["Page"] = page;
            ViewData["Type"] = type;
            return View(manager.GetList(page));
        }

        [HttpGet]
        public ActionResult EditEntity(string type, string id)
        {
            var manager = GetManager(type);

            return PartialView(manager.GetById(id));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SaveEntity(string type, Dictionary<string, string> modelData)
        {
            var manager = GetManager(type);
            var entityType = AAYW.Core.Dependecies.Resolver.GetInstance<IReflector>().Reflect(type).ReflectedType;

            if (manager == null)
            {
                return Json(false);
            }

            var entity = manager.GetById(modelData["Id@System.Guid"]);

            entity = Mapper.Map(entityType, modelData);

            manager.CreateOrUpdate(entity);

            return Json(true);
        }

        [HttpPost]
        public ActionResult DeleteEntity(string id, string type)
        {
            var manager = GetManager(type);
            var entityType = AAYW.Core.Dependecies.Resolver.GetInstance<IReflector>().Reflect(type).ReflectedType;

            if (manager == null)
            {
                return Json(false);
            }

            var entity = manager.GetById(id);

            manager.Delete(entity);

            return Json(true);
        }

        private dynamic GetManager(string type)
        {
            var types = AAYW.Core.Dependecies.Resolver.GetInstance<IReflector>().Reflect(type).ReflectedType;
            var reflectedType = AAYW.Core.Dependecies.Resolver.Managers[types];
            dynamic manager = AAYW.Core.Dependecies.Resolver.GetInstance(reflectedType);

            if (manager == null)
            {
                ModelState.AddModelError("", ResourceAccessor.Instance.Get("EntityNotFound"));
                return View();
            }
            return manager;
        }
        #endregion
    }
}