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
    public class PagesController : AdminBaseController
    {

        public override string Name
        {
            get
            {
                return "AdminPages";
            }
        }

        [HttpGet]
        public ActionResult PagesList(int page)
        {
            var list = SiteApi.Data.Pages.GetList(page);
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
            var model = SiteApi.Data.Pages.GetById(id);

            var mapped = Mapper.Map<PageDesignModel, Page>(model);

            return PartialView("CreatePage", mapped);
        }

        [HttpGet]
        public ActionResult DeletePage(string id)
        {
            var model = SiteApi.Data.Pages.GetById(id);
            if (model != null)
            {
                SiteApi.Data.Pages.Delete(model);
            }
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

            SiteApi.Data.Pages.CreateOrUpdate(mapped);

            return Json(true);
        }
    }
}