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
using AAYW.Core.Extensions;
using System.Reflection;

namespace AAYW.Core.Controller.Concrete
{
    [AccessLevel(Models.Bussines.User.User.Role.Admin)]
    public class AdminController : FrontendController
    {
        public AdminController()
        {

        }

        public override string Name 
        { 
            get {
                return "Admin";
            }
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult EntityInspector(string type)
        {
            var types = Assembly.GetExecutingAssembly().DefinedTypes.Where(x => x.Name.Contains(type + "Manager"));
            if (types.Count() == 0)
            {
                ModelState.AddModelError("", new Exception(ResourceAccessor.Instance.Get("EntityNotFound")));
                return View();
            }            
            var reflectedtype = types.First();
            dynamic manager = typeof(AAYW.Core.Dependecies.Resolver)
                .GetMethod("GetInstance")
                .MakeGenericMethod(new Type[] { reflectedtype })
                .Invoke(null, null);

            if (manager == null)
            {
                ModelState.AddModelError("", new Exception(ResourceAccessor.Instance.Get("EntityNotFound")));
                return View();
            }

            return View(manager.GetList());
        }
    }
}