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
    public class MailSettingsController : AdminBaseController
    {
        public override string Name 
        { 
            get {
                return "AdminMailSettings";
            }
        }
        
        [HttpGet]
        public ActionResult MailSettings()
        {
            var websiteSettings = ((WebsiteSettingsManager)SiteApi.Data.WebsiteSettings).GetSettings();
            var mailSettings = Mapper.Map<MailSettings, WebsiteSetting>(websiteSettings);
            return View(mailSettings);
        }

        [HttpPost]
        public ActionResult MailSettings(MailSettings model)
        {
            if (ModelState.IsValid)
            {
                var websiteSettings = ((WebsiteSettingsManager)SiteApi.Data.WebsiteSettings).GetSettings();
                websiteSettings = Mapper.MapAndMerge<WebsiteSetting, MailSettings>(model, websiteSettings);
                SiteApi.Data.WebsiteSettings.CreateOrUpdate(websiteSettings);
            }

            return View(model);
        }
    }
}