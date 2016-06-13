using AAYW.Core.Models.Bussines.Theme;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AAYW.Core.Web.ViewResults
{
    public class Css : PartialViewResult
    {
        public Css(Theme theme)
        {
            ViewData =  new ViewDataDictionary(theme);
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.ContentType = "text/css";
            base.ExecuteResult(context);
        }
    }
}
