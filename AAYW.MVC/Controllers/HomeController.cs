using AAYW.Core.Data.Managers;
using AAYW.DependecyResolver;
using System.Web.Mvc;
using AAYW.Models;
using AAYW.Resources;
using AAYW.Core.Errors;
using System.Linq;
using System;
using AAYW.Models.ViewModels;

namespace AAYW.MVC.Controllers
{
    public class HomeController : Controller
    {
        UserManager userManager = DependecyResolver.Resolver.GetInstance<UserManager>();
        QuestionManager questionManager = DependecyResolver.Resolver.GetInstance<QuestionManager>();

        [HttpGet]
        public ActionResult Index()
        {
            if (Request.Cookies.AllKeys.Contains("aayw-landed"))
            {
                return View(questionManager.GetList());
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
            if (string.IsNullOrWhiteSpace(model.confirmation))
            {
                model.confirmation = string.Empty;
            }

            if (string.IsNullOrWhiteSpace(model.login) || model.login.Length > 50)
            {
                ModelState.AddModelError("login", string.Format(ResourceAccessor.Instance.Get("MaxLength"), "Login", 50));
            }
            else if (string.IsNullOrWhiteSpace(model.password) || model.password.Length > 50)
            {
                ModelState.AddModelError("password", string.Format(ResourceAccessor.Instance.Get("MaxLength"), "Password", 50));
            }
            else if (!model.password.Equals(model.confirmation))
            {
                ModelState.AddModelError("password", ResourceAccessor.Instance.Get("PasswordsAreNotEqual"));
            }
            else if (!userManager.IsAvalibleForCreation(model.login))
            {
                ModelState.AddModelError("login", ResourceAccessor.Instance.Get("UserAlreadyRegistered"));
            }

            if (!ModelState.IsValid)
            {
                return View();
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
        public ActionResult Login(string login, string password)
        {
            if (!userManager.Login(login, password))
            {
                ModelState.AddModelError("", ResourceAccessor.Instance.Get("FailedToLogin"));
            }

            if (!ModelState.IsValid)
            {
                return View();
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