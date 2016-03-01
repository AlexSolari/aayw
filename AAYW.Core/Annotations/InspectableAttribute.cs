using AAYW.Core.Data.Managers;
using AAYW.Core.Dependecies;
using AAYW.Core.Models.Bussines.User;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace AAYW.Core.Annotations
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class InspectableAttribute : Attribute
    {
        public InspectableAttribute() { }
    }
}
