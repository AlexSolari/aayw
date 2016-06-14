using AAYW.Core.Models.Bussines;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAYW.Core.Models.Bussines.User;
using AAYW.Core.Models.Admin.Bussines;

namespace AAYW.Core.Data.Providers
{
    public class WebsiteSettingsProvider : BaseProvider<WebsiteSetting>
    {
        public WebsiteSettingsProvider() : base() { }
        public WebsiteSettingsProvider(bool suppressLogging) : base(suppressLogging) { }
    }
}
