using AAYW.Core.Models.Bussines.Admin;
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
    class UserFormMap : EntityMap<UserForm>
    {
        public UserFormMap()
            : base()
        {
            Map(x => x.Fields).CustomSqlType("xml");
            Map(x => x.Url).Length(200);
            Map(x => x.MailTemplateName).Length(100);
            Map(x => x.SubmitAdress).Length(200);
            Map(x => x.Header).Length(200);
            CrateTable();
        }
    }
}
