using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using AAYW.Core.Extensions;

namespace AAYW.Core.Web.Controller
{
    public abstract class FrontendController : System.Web.Mvc.Controller
    {
        public FrontendController()
    	{

	    }

        public abstract string Name { get; }
    }
}
