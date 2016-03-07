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

namespace AAYW.Core.Controller.Concrete
{
    [AccessLevel(Models.Bussines.User.User.Role.Admin)]
    public class AdminController : FrontendController
    {
        WebsiteSettingsManager settingsManager = (WebsiteSettingsManager)AAYW.Core.Dependecies.Resolver.GetInstance<IManager<WebsiteSetting>>();
        IManager<MailTemplate> mailTemplatesManager = AAYW.Core.Dependecies.Resolver.GetInstance<IManager<MailTemplate>>();
        IManager<UserForm> userFormsManager = AAYW.Core.Dependecies.Resolver.GetInstance<IManager<UserForm>>();

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
        #region CustomForm
        [HttpGet]
        public ActionResult CustomForm(string url)
        {
            var form = userFormsManager.GetByField("Url", url);

            if (form == null)
            {
                return RedirectToRoute("Error404");
            }

            var mapped = Mapper.Map<UserFormDesignModel, UserForm>(form);

            return View(mapped);
        }

        [HttpPost]
        public ActionResult CustomForm(dynamic model)
        {
            throw new NotImplementedException();
        }

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
            return PartialView(Dependecies.Resolver.GetInstance<UserFormDesignModel>());
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
                return View(model);
            }

            var mapped = Mapper.Map<UserForm, UserFormDesignModel>(model);

            if (((UserFormManager)userFormsManager).IsAvalibleForCreation(mapped))
            {
                userFormsManager.CreateOrUpdate(mapped);
                return Json(true);
            }
            
            return Json(false);
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
        public ActionResult SaveEntity(string type, Dictionary<string, string> modelData)
        {
            var manager = GetManager(type);
            var entityType = Assembly.GetExecutingAssembly().DefinedTypes.Where(x => x.Name.Equals(type)).FirstOrDefault();

            if (manager == null)
            {
                return Json(false);
            }

            var entity = manager.GetById(modelData["Id@System.Guid"]);

            entity = Mapper.Map(entityType, modelData);

            return Json(manager.Update(entity));
        }

        private dynamic GetManager(string type)
        {
            var types = (Type)Assembly.GetExecutingAssembly().DefinedTypes.Where(x => x.Name.Equals(type)).FirstOrDefault();
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