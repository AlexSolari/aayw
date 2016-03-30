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
    class ContentBlockMap : EntityMap<ContentBlock>
    {
        public ContentBlockMap()
            : base()
        {
            Map(x => x.Type);
            Map(x => x.Content).CustomSqlType("text");
            CrateTable();
        }
    }
}
