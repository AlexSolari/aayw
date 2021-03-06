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
using AAYW.Core.Models.Bussines.User;
using AAYW.Core.Models.Bussines.Admin;
using AAYW.Core.Api;
using AAYW.Core.Web.ViewResults;
using AAYW.Core.Cache;
using AAYW.Core.Models.Bussines.Theme;
using AAYW.Core.Annotations;

namespace AAYW.Core.Controller.Concrete
{
    public class HomeController : FrontendController
    {
        public HomeController()
        {

        }

        public override string Name 
        { 
            get {
                return "Home";
            }
        }

        [HttpGet]
        public ActionResult Theme()
        {
            if (SiteApi.Services.Cache.HasKey("CssTheme"))
            {
                return SiteApi.Services.Cache.Get<Css>("CssTheme");
            }

            var theme = SiteApi.Frontend.CurrentTheme;

            var css = new Css(theme);
            SiteApi.Services.Cache.Add(css, "CssTheme");

            return css;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var homePage = SiteApi.Data.Pages.GetByField("Url", "home");
            if (Request.Cookies.AllKeys.Contains("aayw-landed") && homePage != null)
            {
                return RedirectToRoute("CustomPage", new { url = "home"});
            }

            return RedirectToRoute("Landing");
        }

        [HttpGet]
        public ActionResult Landing()
        {
            Request.RequestContext.HttpContext.Response.Cookies.Add(new System.Web.HttpCookie("aayw-landed", Guid.NewGuid().ToString()));

            return View();
        }

        #region SignUp/SignOut

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegistrationModel model)
        {
            if (!SiteApi.Data.Users.IsAvalibleForCreation(model.Login))
            {
                ModelState.AddModelError("login", SiteApi.Texts.Get("UserAlreadyRegistered"));
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.Login = model.Login.Trim();
            model.Password = model.Password.Trim();
            model.Confirmation = model.Confirmation.Trim();

            if (SiteApi.Data.Users.Register(model.Login, model.Password))
            {
                return RedirectToRoute("Login");
            }

            throw new SystemException("Unknown error occured while processing request");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (!SiteApi.Data.Users.Login(model.Login, model.Password))
            {
                ModelState.AddModelError("", SiteApi.Texts.Get("FailedToLogin"));
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return RedirectToRoute("Home");
        }

        [HttpGet]
        public ActionResult Logout()
        {
            SiteApi.Data.Users.Logout();

            return RedirectToRoute("Home");
        }

        #endregion
    }
}