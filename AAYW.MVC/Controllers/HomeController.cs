using AAYW.Core.Data.Managers;
using AAYW.Core.Dependecies;
using System.Web.Mvc;
using AAYW.Models;
using AAYW.Resources;
using AAYW.Core.Errors;
using System.Linq;
using System;
using AAYW.ViewModels;

namespace AAYW.MVC.Controllers
{
    public class HomeController : Controller
    {
        UserManager userManager = AAYW.Core.Dependecies.Resolver.GetInstance<UserManager>();

        [HttpGet]
        public ActionResult Index()
        {
            if (Request.Cookies.AllKeys.Contains("aayw-landed"))
            {
                return View();
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
            if (!userManager.IsAvalibleForCreation(model.login))
            {
                ModelState.AddModelError("login", ResourceAccessor.Instance.Get("UserAlreadyRegistered"));
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.login = model.login.Trim();
            model.password = model.password.Trim();
            model.confirmation = model.confirmation.Trim();

            if (userManager.Register(model.login, model.password))
            {
                return RedirectToRoute("Login");
            }

            return RedirectToRoute("GenericError", new { errorCode = Errors.RegistrationFailed });
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (!userManager.Login(model.login, model.password))
            {
                ModelState.AddModelError("", ResourceAccessor.Instance.Get("FailedToLogin"));
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
            userManager.Logout();

            return RedirectToRoute("Home");
        }

        #endregion
    }
}