using AAYW.Core.Models.Bussines.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Models.Mappings
{
    class PostMap : EntityMap<Post>
    {
        public PostMap()
            : base()
        {
            Map(x => x.Title).Length(500);
            Map(x => x.Content).CustomSqlType("text").Length(5000);
            Map(x => x.FeedId);
            CrateTable();
        }
    }
}
