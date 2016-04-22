using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Logging
{
    public interface ILogger
    {
        void Log(string message);
        IEnumerable<string> GetLog();
        IEnumerable<string> GetLog(int count);
    }
}
