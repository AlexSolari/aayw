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

namespace AAYW.Core.Controller.Concrete
{
    public class ErrorController : FrontendController
    {
        public ErrorController()
        {

        }

        public override string Name 
        { 
            get {
                return "Error";
            }
        }

        [HttpGet]
        public ActionResult Error404()
        {
            Response.StatusCode = 404;
            return View();
        }
    }
}