using AAYW.Core.Data.Managers;
using AAYW.Core.Mail;
using AAYW.Core.Map;
using AAYW.Core.Models.Bussines.Admin;
using AAYW.Core.Models.View.UserForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AAYW.Core.Extensions;

namespace AAYW.Core.Web.Controller.Concrete
{
    public class UserFormController : FrontendController
    {
        IManager<UserForm> userFormsManager = AAYW.Core.Dependecies.Resolver.GetInstance<IManager<UserForm>>();

        public override string Name
        {
            get { return "UserForm"; }
        }

        [HttpGet]
        public ActionResult CustomForm(string url)
        {
            var form = userFormsManager.GetByField("Url", url);

            if (form == null)
            {
                return RedirectToRoute("Error404");
            }

            var mapped = Mapper.Map<UserFormDesignModel, UserForm>(form);

            return View(mapped);
        }

        [HttpPost]
        public ActionResult CustomForm(string url, Dictionary<string, object> model)
        {
            var form = userFormsManager.GetByField("Url", url);
            var mapped = Mapper.Map<UserFormDesignModel, UserForm>(form);

            var replacements = new Dictionary<string, string>();

            foreach (var field in model)
            {              
                replacements.Add(field.Key, ((string[])field.Value).FirstOrDefault().SafeToString());
            }

            var mailer = Dependecies.Resolver.GetInstance<IMailProcessor>();

            mailer.Send(form.SubmitAdress, form.Header, form.MailTemplateName, replacements);

            return RedirectToRoute("FormSubmited");
        }

        [HttpGet]
        public ActionResult FormSubmited()
        {
            return View();
        }
    }
}
