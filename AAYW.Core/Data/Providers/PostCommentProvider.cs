﻿using AAYW.Core.Models.Bussines;
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
    public class PostCommentProvider : BaseProvider<PostComment>
    {
        public PostCommentProvider() : base() { }
        public PostCommentProvider(bool suppressLogging) : base(suppressLogging) { }
    }
}
