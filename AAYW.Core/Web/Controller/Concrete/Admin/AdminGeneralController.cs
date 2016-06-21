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
    public class GeneralController : AdminBaseController
    {
        public GeneralController()
        {

        }

        public override string Name 
        { 
            get {
                return "AdminGeneral";
            }
        }

        public ActionResult ChangeLogo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangeLogo(HttpPostedFileBase small, HttpPostedFileBase big, HttpPostedFileBase favicon)
        {
            if (small != null && small.ContentLength > 0)
            {
                try
                {
                    var path = Path.Combine(Server.MapPath("~/Content/img"), "logo.png");
                    small.SaveAs(path);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("small", ex.Message.ToString());
                    return View();
                }
            }

            if (big != null && big.ContentLength > 0)
            {
                try
                {
                    var path = Path.Combine(Server.MapPath("~/Content/img"), "logo-big.png");
                    big.SaveAs(path);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("big", ex.Message.ToString());
                    return View();
                }
            }

            if (favicon != null && favicon.ContentLength > 0)
            {
                try
                {
                    var path = Path.Combine(Server.MapPath("~/Content/img"), "favicon.png");
                    favicon.SaveAs(path);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("favicon", ex.Message.ToString());
                    return View();
                }
            }

            if (big == null && small == null && favicon == null)
            {
                ModelState.AddModelError("", SiteApi.Texts.Get("FileNotSpecified"));
                return View();
            }

            return View();
        }

        public ActionResult ChangeColorTheme()
        {
            return View(SiteApi.Frontend.CurrentTheme);
        }

        [HttpPost]
        public ActionResult ChangeColorTheme(Theme newTheme)
        {
            SiteApi.Frontend.CurrentTheme = newTheme;
            SiteApi.Services.Cache.Drop("CssTheme");

            return View("ChangeColorTheme", newTheme);
        }

        public ActionResult ResetColorTheme()
        {
            ChangeColorTheme(SiteApi.Frontend.DefaultTheme);

            return RedirectToAction("ChangeColorTheme");
        }

    }
}