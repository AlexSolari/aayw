using AAYW.Core.Models.Bussines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAYW.Core.Models.Bussines.User;
using AAYW.Core.Models.Bussines.Admin;

namespace AAYW.Core.Data.Providers
{
    public class PageProvider : BaseProvider<Page>
    {
        public PageProvider() : base() { }
        public PageProvider(bool suppressLogging) : base(suppressLogging) { }
    }
}
