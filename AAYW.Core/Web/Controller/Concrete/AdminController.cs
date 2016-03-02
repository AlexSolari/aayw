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

namespace AAYW.Core.Controller.Concrete
{
    [AccessLevel(Models.Bussines.User.User.Role.Admin)]
    public class AdminController : FrontendController
    {
        WebsiteSettingsManager settingsManager = (WebsiteSettingsManager)AAYW.Core.Dependecies.Resolver.GetInstance<IManager<WebsiteSettings>>();

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

        [HttpGet]
        public ActionResult MailSettings()
        {
            var websiteSettings = settingsManager.GetSettings();
            var mailSettings = Mapper.Map<MailSettings, WebsiteSettings>(websiteSettings);
            return View(mailSettings);
        }

        [HttpPost]
        public ActionResult MailSettings(MailSettings model)
        {
            var websiteSettings = settingsManager.GetSettings();
            websiteSettings = Mapper.MapAndMerge<WebsiteSettings, MailSettings>(model, websiteSettings);
            settingsManager.UpdateSettings(websiteSettings);
            return View(model);
        }

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
    }
}