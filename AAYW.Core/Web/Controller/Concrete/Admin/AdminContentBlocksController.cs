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
    public class ContentBlocksController : AdminBaseController
    {        
        public override string Name 
        { 
            get {
                return "AdminContentBlocks";
            }
        }

        [HttpGet]
        public ActionResult ContentBlocksList(int page)
        {
            var list = SiteApi.Data.ContentBlocks.GetList(page);
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
            var model = SiteApi.Data.ContentBlocks.GetById(id);

            var mapped = Mapper.Map<ContentBlockDesignModel, ContentBlock>(model);

            return PartialView("CreateContentBlock", mapped);
        }

        [HttpGet]
        public ActionResult DeleteContentBlock(string id)
        {
            var model = SiteApi.Data.ContentBlocks.GetById(id);
            if (model != null)
            {
                SiteApi.Data.ContentBlocks.Delete(model);
            }
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

            SiteApi.Data.ContentBlocks.CreateOrUpdate(mapped);

            return Json(true);
        }
    }
}