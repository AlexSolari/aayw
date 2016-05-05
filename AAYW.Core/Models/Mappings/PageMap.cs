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
    class PageMap : EntityMap<Page>
    {
        public PageMap() : base()
        {
            Map(x => x.ContentBlocks).CustomSqlType("xml");
            Map(x => x.Title).Length(300);
            Map(x => x.Url).Length(100);
            CrateTable();
        }
    }
}
