using AAYW.Core.Annotations;
using AAYW.Core.Data.Providers;
using AAYW.Core.Models.Bussines.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Data.Managers
{
    [ManagerFor(typeof(Page))]
    public class PageManager : BaseManager<PageProvider, Page>, IManager<Page>
    {
        public PageManager() : base() { }
        public PageManager(bool suppressLogging) : base(suppressLogging) { }
    }
}
