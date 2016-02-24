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
using System.Web;

namespace AAYW.Core.Extensions
{
    public static class HtmlHelperExtensions
    {
        #region Text
		public static string Text<T>(this HtmlHelper<T> val, string key)
        {
            return ResourceAccessor.Instance.Get(key);
        }

        public static HtmlString RichText<T>(this HtmlHelper<T> val, string key)
        {
            return new HtmlString(ResourceAccessor.Instance.Get(key));
        }
	    #endregion
        
        #region Buttons
        public static HtmlString Button<T>(this HtmlHelper<T> val, string text, ButtonType type = ButtonType.Flat, string href = "#")
        {
            var cssClass = "";
            switch (type)
            {
                case ButtonType.Raised:
                    cssClass = "mui-btn--raised";
                    break;
                case ButtonType.Flat:
                default:
                    cssClass = "mui-btn--flat";
                    break;
            }
            return new HtmlString("<a href='{0}' class='mui-btn {1}'>{2}</a>".FormatWith(href, cssClass, text));
        }

        public static HtmlString Submit<T>(this HtmlHelper<T> val, string text, ButtonType type = ButtonType.Flat)
        {
            var cssClass = "";
            switch (type)
            {
                case ButtonType.Raised:
                    cssClass = "mui-btn--raised";
                    break;
                case ButtonType.Flat:
                default:
                    cssClass = "mui-btn--flat";
                    break;
            }
            return new HtmlString("<input class='mui-btn {0}' type='submit' value='{1}' />".FormatWith(cssClass, text));
        }
	    #endregion
        
    }

    public enum ButtonType
    {
        Flat,
        Raised
    }
}
