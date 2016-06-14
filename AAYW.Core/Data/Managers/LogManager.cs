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
    [ManagerFor(typeof(LogMessage))]
    public class LogManager : BaseManager<LogMessageProvider, LogMessage>, IManager<LogMessage>
    {
        public LogManager() : base() { }
        public LogManager(bool suppressLogging) : base(suppressLogging) { }
    }
}
