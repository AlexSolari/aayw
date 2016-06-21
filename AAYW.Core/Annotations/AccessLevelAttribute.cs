using AAYW.Core.Api;
using AAYW.Core.Data.Managers;
using AAYW.Core.Dependecies;
using AAYW.Core.Models.Bussines.User;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace AAYW.Core.Annotations
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class AccessLevelAttribute : ActionFilterAttribute, IActionFilter
    {
        public User.Role Role { get; set; }

        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            var userManager = (UserManager)SiteApi.Data.Users;
            if (userManager.CurrentUser == null || userManager.CurrentUser.CurrentRole != Role)
            {
                filterContext.Result = new ViewResult() { ViewName = "Error403" };
            }
        }

        public AccessLevelAttribute(User.Role Role)
        {
            this.Role = Role;
        }
    }
}
