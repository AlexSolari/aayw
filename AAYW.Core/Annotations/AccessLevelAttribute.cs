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
        public string Role { get; set; }

        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            var CurrentUser = SiteApi.Data.Users.CurrentUser;
            if (CurrentUser == null || CurrentUser.CurrentRole != Role)
            {
                filterContext.Result = new ViewResult() { ViewName = "Error403" };
            }
        }

        public AccessLevelAttribute(string Role)
        {
            this.Role = Role;
        }
    }
}
