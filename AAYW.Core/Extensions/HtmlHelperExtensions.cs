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
using System.IO;

namespace System
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
        public static HtmlString Button<T>(this HtmlHelper<T> val, string text, ButtonType type = ButtonType.Flat, string href = "#", string cssClass = "")
        {
            cssClass += " mui-btn";
            switch (type)
            {
                case ButtonType.Raised:
                    cssClass += " mui-btn--raised";
                    break;
                case ButtonType.Flat:
                default:
                    cssClass += " mui-btn--flat";
                    break;
            }
            return new HtmlString("<a href='{0}' class='{1}'>{2}</a>".FormatWith(href, cssClass, text));
        }

        public static HtmlString Submit<T>(this HtmlHelper<T> val, string text, ButtonType type = ButtonType.Flat, string cssClass = "")
        {
            cssClass += " mui-btn";
            switch (type)
            {
                case ButtonType.Raised:
                    cssClass += " mui-btn--raised";
                    break;
                case ButtonType.Flat:
                default:
                    cssClass += " mui-btn--flat";
                    break;
            }
            return new HtmlString("<input class='{0}' type='submit' value='{1}' />".FormatWith(cssClass, text));
        }
	    #endregion

        #region Logos

        public static HtmlString Logo<T>(this HtmlHelper<T> val, Size size, string cssClass = "", string alt = "")
        {
            var template = "<img {0} />";
            var inner = "";

            switch (size)
            {
                case Size.Small:
                    inner += "src=\"{0}\" ".FormatWith("/Content/img/logo.png");
                    break;
                case Size.Big:
                    inner += "src=\"{0}\" ".FormatWith("/Content/img/logo-big.png");
                    break;
                default:
                    break;
            }

            inner += "class=\"{0}\"".FormatWith(cssClass);
            inner += "alt=\"{0}\"".FormatWith(alt);

            return new HtmlString(template.FormatWith(inner));
        }

        #endregion
    }

    public enum Size
    {
        Small,
        Big
    }

    public enum ButtonType
    {
        Flat,
        Raised
    }
}
