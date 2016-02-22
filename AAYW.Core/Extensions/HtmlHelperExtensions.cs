using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc.Html;
using AAYW.Resources;
using System.Text.RegularExpressions;
using System.Linq.Expressions;

namespace AAYW.Core.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static string Text<T>(this HtmlHelper<T> val, string key)
        {
            return ResourceAccessor.Instance.Get(key);
        }
    }
}
