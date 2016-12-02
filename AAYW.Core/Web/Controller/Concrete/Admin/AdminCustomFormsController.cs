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
    public class CustomFormsController : AdminBaseController
    {
        public override string Name 
        { 
            get {
                return "AdminCustomForms";
            }
        }

        [HttpGet]
        public ActionResult CustomFormsList(int page)
        {
            var list = SiteApi.Data.UserForms.GetList(page);
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
            var model = SiteApi.Data.UserForms.GetById(id);

            var mapped = Mapper.Map<UserFormDesignModel, UserForm>(model);
            ViewData.Add("editmode", true);

            return PartialView("CreateCustomForm", mapped);
        }

        [HttpGet]
        public ActionResult DeleteUserForm(string id)
        {
            var model = SiteApi.Data.UserForms.GetById(id);
            if (model != null)
            {
                SiteApi.Data.UserForms.Delete(model);
            }
            return RedirectToRoute("CustomFormsList", new { page = 0 });
        }

        [HttpPost]
        public ActionResult CreateCustomForm(UserFormDesignModel model, bool isEditing)
        {
            if (!isEditing && !ModelState.IsValid)
            {
                return PartialView(model);
            }

            var mapped = Mapper.Map<UserForm, UserFormDesignModel>(model);

            if (isEditing || SiteApi.Data.UserForms.IsAvalibleForCreation(mapped))
            {
                SiteApi.Data.UserForms.CreateOrUpdate(mapped);
            }
            else
            {
                ModelState.AddModelError("Url", SiteApi.Texts.Get("Error_UrlMustBeUniue"));
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
    }
}