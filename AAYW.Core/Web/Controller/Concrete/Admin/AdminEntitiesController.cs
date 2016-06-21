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
using AAYW.Core.Api;
using System.Web;
using System.IO;
using AAYW.Core.Models.Bussines.Theme;
using AAYW.Core.Web.ViewResults;

namespace AAYW.Core.Controller.Concrete.Admin
{
    public class EntitiesController : AdminBaseController
    {
        
        public override string Name 
        { 
            get {
                return "AdminEntities";
            }
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
        [ValidateInput(false)]
        public ActionResult SaveEntity(string type, Dictionary<string, string> modelData)
        {
            var manager = GetManager(type);
            var entityType = SiteApi.Services.Reflector.Reflect(type).ReflectedType;

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
            var entityType = SiteApi.Services.Reflector.Reflect(type).ReflectedType;

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
            var types = SiteApi.Services.Reflector.Reflect(type).ReflectedType;
            var reflectedType = AAYW.Core.Dependecies.Resolver.Managers[types];
            dynamic manager = AAYW.Core.Dependecies.Resolver.GetInstance(reflectedType);

            if (manager == null)
            {
                ModelState.AddModelError("", SiteApi.Texts.Get("EntityNotFound"));
                return View();
            }
            return manager;
        }
    }
}