using AAYW.Core.Data.Managers;
using AAYW.ViewModels;
using AAYW.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AAYW.MVC.Controllers
{
    public class QuestionController : Controller
    {
        UserManager userManager = DependecyResolver.Resolver.GetInstance<UserManager>();
        QuestionManager questionManager = DependecyResolver.Resolver.GetInstance<QuestionManager>();
        AnswerManager answerManager = DependecyResolver.Resolver.GetInstance<AnswerManager>();

        [HttpGet]
        public ActionResult Index(string id)
        {
            if (questionManager.GetById(id) == null)
                return RedirectToRoute("Home");

            questionManager.IncrementViews(id);
            return View(questionManager.GetById(id));
        }

        [HttpPost]
        public ActionResult Index(AnswerModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Description) || model.Description.Length > 1000)
            {
                ModelState.AddModelError("Description", string.Format(ResourceAccessor.Instance.Get("MaxLength"), "Description", 1000, 0));
            }

            if (!ModelState.IsValid)
            {
                return RedirectToRoute("Question", new { id = model.ParentId.ToString() });
            }

            answerManager.Create(model.Description, model.ParentId);

            return RedirectToRoute("Question", new { id = model.ParentId.ToString() });
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateQuestionModel model)
        {
            var authorized = userManager.IsAuthorized;
            var maxLengthDescription = (authorized) ? 1000 : 200;
            var maxLengthTitle = (authorized) ? 400 : 80;

            if (string.IsNullOrWhiteSpace(model.Description) || model.Description.Length > maxLengthDescription)
            {
                ModelState.AddModelError("Description", string.Format(ResourceAccessor.Instance.Get("MaxLength"), "Description", maxLengthDescription, 0));
            }
            if (string.IsNullOrWhiteSpace(model.Title) || model.Title.Length > maxLengthTitle)
            {
                ModelState.AddModelError("Title", string.Format(ResourceAccessor.Instance.Get("MaxLength"), "Title", maxLengthTitle, 0));
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.Description = model.Description.Trim();
            model.Title = model.Title.Trim();

            var user = userManager.CurrentUser;
            var newQuestion = questionManager.Create(model.Description, model.Title, user);

            if (newQuestion == null)
                return RedirectToRoute("CreateQuestion");

            return RedirectToRoute("Question", new { id = newQuestion.Id.ToString() });
        }
    }
}