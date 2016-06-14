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
    [ManagerFor(typeof(Post))]
    public class PostManager : BaseManager<PostProvider, Post>, IManager<Post>
    {
        public PostManager() : base() { }
        public PostManager(bool suppressLogging) : base(suppressLogging) { }
    }
}
