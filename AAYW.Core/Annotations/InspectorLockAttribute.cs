using AAYW.Core.Data.Managers;
using AAYW.Core.Dependecies;
using AAYW.Core.Models.Bussines.User;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace AAYW.Core.Annotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class InspectorLockAttribute : Attribute
    {
        public InspectorLockAttribute() { }
    }
}
