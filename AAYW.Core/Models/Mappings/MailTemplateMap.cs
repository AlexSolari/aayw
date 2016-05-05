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
    class MailTemplateMap : EntityMap<MailTemplate>
    {
        public MailTemplateMap()
            : base()
        {
            Map(x => x.Name).Length(100);
            Map(x => x.Body).Length(2000);
            CrateTable();
        }
    }
}
