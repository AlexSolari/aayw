﻿using AAYW.Core.Data.Managers;
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
    public class LoggingController : AdminBaseController
    {
        
        public override string Name 
        { 
            get {
                return "AdminLogging";
            }
        }

        [HttpGet]
        public ActionResult EntireLog()
        {
            return View("Log", SiteApi.Services.Logger.GetLog());
        }

        [HttpGet]
        public ActionResult Log(int count)
        {
            return View(SiteApi.Services.Logger.GetLog(count));
        }
    }
}