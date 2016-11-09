using AAYW.Core.Api;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AAYW.Core.Annotations
{
    public class PerfomanceProfilingAttribute : ActionFilterAttribute, IActionFilter
    {
        private Stopwatch Timer { get; set; }

        public PerfomanceProfilingAttribute()
        {
            Timer = new Stopwatch();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Timer.Start();
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Timer.Stop();
            SiteApi.Services.Logger.Log("PERFOMANCE: Request at [{0}] taken {1} ms ({2} ticks)".FormatWith(filterContext.HttpContext.Request.RawUrl, Timer.ElapsedMilliseconds, Timer.ElapsedTicks));
            Timer.Reset();
        }
    }
}

