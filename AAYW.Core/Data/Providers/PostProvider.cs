using AAYW.Core.Models.Bussines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAYW.Core.Models.Bussines.User;
using AAYW.Core.Models.Bussines.Admin;
using AAYW.Core.Models.Bussines.Post;

namespace AAYW.Core.Data.Providers
{
    public class PostProvider : BaseProvider<Post>
    {
        public PostProvider() : base() { }
        public PostProvider(bool suppressLogging) : base(suppressLogging) { }
    }
}
