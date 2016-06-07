using AAYW.Core.Data.Managers;
using AAYW.Core.Dependecies;
using AAYW.Core.Models.Admin.Bussines;
using AAYW.Core.Models.Bussines.Admin;
using AAYW.Core.Models.Bussines.Post;
using AAYW.Core.Models.Bussines.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Api
{
    public static partial class SiteApi
    {
        public static DataApi Data
        {
            get { return DataApi.Instance; }
        }

        public static SiteServiceApi Services
        {
            get { return SiteServiceApi.Instance; }
        }

        public static TextsApi Texts
        {
            get { return TextsApi.Instance; }
        }
    }
}
