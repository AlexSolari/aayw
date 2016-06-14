using AAYW.Core.Annotations;
using AAYW.Core.Data.Providers;
using AAYW.Core.Models.Bussines.Admin;
using AAYW.Core.Models.Bussines.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Data.Managers
{
    [ManagerFor(typeof(PostComment))]
    public class PostCommentManager : BaseManager<PostCommentProvider, PostComment>, IManager<PostComment>
    {
        public PostCommentManager() : base() { }
        public PostCommentManager(bool suppressLogging) : base(suppressLogging) { }
    }
}
