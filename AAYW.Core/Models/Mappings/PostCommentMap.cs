using AAYW.Core.Models.Bussines.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Models.Mappings
{
    class PostCommentMap : EntityMap<PostComment>
    {
        public PostCommentMap()
            : base()
        {
            Map(x => x.Content).Length(500);
            Map(x => x.UserId);
            Map(x => x.PostId);
            CrateTable();
        }
    }
}
