using AAYW.Core.Api;
using AAYW.Core.Data.Managers;
using AAYW.Core.Dependecies;
using AAYW.Core.Models.Bussines.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Logging
{
    public class Logger : ILogger
    {
        IManager<LogMessage> manager = SiteApi.Data.LogMessages;

        public void Log(string message)
        {
            var msg = Resolver.GetInstance<LogMessage>();
            msg.Message = "[{0}] {1}".FormatWith(DateTime.Now.ToString() ,message);
            manager.CreateOrUpdate(msg);
        }

        public IEnumerable<string> GetLog()
        {
            return manager.All().OrderByDescending(x => x.CreatedDate).Select(x => x.Message);
        }

        public IEnumerable<string> GetLog(int count)
        {
            return manager.GetList(0, count).OrderByDescending(x => x.CreatedDate).Select(x => x.Message);
        }
    }
}
