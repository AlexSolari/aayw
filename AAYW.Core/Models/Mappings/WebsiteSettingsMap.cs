using AAYW.Core.Models.Admin.Bussines;
using AAYW.Core.Models.Bussines;
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
    class WebsiteSettingsMap : EntityMap<WebsiteSetting>
    {
        public WebsiteSettingsMap() : base()
        {
            Map(x => x.MailEnableSsl);
            Map(x => x.MailHost).Length(200);
            Map(x => x.MailPassword).Length(200);
            Map(x => x.MailUsername).Length(200);
            Map(x => x.MailPort);
            Map(x => x.MailAdress).Length(200);
            Map(x => x.CurrentTheme).CustomSqlType("xml");
            CrateTable();
        }
    }
}
