using AAYW.Core.Api;
using AAYW.Core.Data.Managers;
using AAYW.Core.Dependecies;
using AAYW.Core.Models.Bussines.Admin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AAYW.Core.Logging
{
    public class Logger : ILogger
    {
        public void Log(string message)
        {
            lock (this)
            {
                using (var writer = new StreamWriter(HttpContext.Current.Server.MapPath("~/Logs/log#{0}.txt".FormatWith(DateTime.Now.ToShortDateString().Replace('/', '.'))), true))
                {
                    writer.WriteLine("[{0}] {1}".FormatWith(DateTime.Now.ToString(), message));
                }
            }
        }

        public IEnumerable<string> GetLog()
        {
            var result = new List<string>();
            using (var reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Logs/log#{0}.txt".FormatWith(DateTime.Now.ToShortDateString().Replace('/', '.')))))
            {
                while (!reader.EndOfStream)
	            {
                    result.Add(reader.ReadLine());
	            }
                
            }
            result.Reverse();
            return result;
        }

        public IEnumerable<string> GetLog(int count)
        {
            var log = GetLog();
            return log.Take(count);
        }
    }
}
