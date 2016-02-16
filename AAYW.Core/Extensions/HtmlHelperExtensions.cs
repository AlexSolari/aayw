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

        public static MvcHtmlString ResourceLabelFor<T, TValue>(this HtmlHelper<T> val, Expression<Func<T, TValue>> expression)
        {
            var proxyValue = val.LabelFor(expression).ToString();
            var groups = Regex.Match(proxyValue, "<[\\w\\s=\"]*>([\\w\\s-.,]*)<\\/[\\w\\s=\"]*>");
            var resourceName = groups.Groups[1].Value;
            var name = ResourceAccessor.Instance.Get(resourceName);
            var result = string.Format("<label>{0}</label>", name);
            return new MvcHtmlString(result);
        }
    }
}
