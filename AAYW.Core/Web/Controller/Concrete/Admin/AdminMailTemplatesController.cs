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
    public class MailTemplatesController : AdminBaseController
    {
        public override string Name 
        { 
            get {
                return "AdminMailTemplates";
            }
        }
        
        [HttpGet]
        public ActionResult MailTemplates(int page)
        {
            ViewData["Page"] = page;
            var templates = SiteApi.Data.MailTemplates.GetList(page);
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
            var template = Mapper.Map<MailTemplateCreateModel, MailTemplate>(SiteApi.Data.MailTemplates.GetById(id));
            return PartialView("_MailTemplateCreate", template);
        }

        [HttpPost]
        public ActionResult CreateOrUpdateMailTemplate(MailTemplateCreateModel model)
        {
            var template = Mapper.Map<MailTemplate, MailTemplateCreateModel>(model);

            if (!((MailTemplateManager)SiteApi.Data.MailTemplates).CanCreate(template))
            {
                ModelState.AddModelError("Name", SiteApi.Texts.Get("Error_TemplateExist"));
            }

            if (!ModelState.IsValid)
            {
                return PartialView("_MailTemplateCreate", model);
            }

            SiteApi.Data.MailTemplates.CreateOrUpdate(template);

            return Json(false);
        }

        public ActionResult DeleteMailTemplate(string id)
        {
            var model = SiteApi.Data.MailTemplates.GetById(id);
            if (model != null)
            {
                SiteApi.Data.MailTemplates.Delete(model);
            }
            return RedirectToRoute("MailTemplates", new { page = 0 });
        }
    }
}