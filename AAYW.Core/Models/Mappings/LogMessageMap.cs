using AAYW.Core.Models.Bussines.Admin;
using AAYW.Core.Models.Bussines.User;
using AAYW.Core.Models.Mappings;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Models.Mappings
{
    class LogMessageMap : EntityMap<LogMessage>
    {
        public LogMessageMap()
            : base()
        {
            Map(x => x.Message).Length(200);
            CrateTable();
        }
    }
}
