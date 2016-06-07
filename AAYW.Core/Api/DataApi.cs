using AAYW.Core.Data.Managers;
using AAYW.Core.Dependecies;
using AAYW.Core.Models.Admin.Bussines;
using AAYW.Core.Models.Bussines.Admin;
using AAYW.Core.Models.Bussines.Post;
using AAYW.Core.Models.Bussines.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Api
{
    public static partial class SiteApi
    {
        public class DataApi
        {
            private DataApi()
            {

            }

            private static DataApi _instance;

            public static DataApi Instance
            {
                get
                {
                    if (_instance == null)
                        _instance = new DataApi();
                    return _instance;
                }
            }

            public IManager<Post> Posts
            {
                get { return Resolver.GetInstance<IManager<Post>>(); }
            }

            public IManager<User> Users
            {
                get { return Resolver.GetInstance<IManager<User>>(); }
            }

            public IManager<Page> Pages
            {
                get { return Resolver.GetInstance<IManager<Page>>(); }
            }

            public IManager<LogMessage> LogMessages
            {
                get { return Resolver.GetInstance<IManager<LogMessage>>(); }
            }

            public IManager<UserForm> UserForms
            {
                get { return Resolver.GetInstance<IManager<UserForm>>(); }
            }

            public IManager<PostComment> PostComments
            {
                get { return Resolver.GetInstance<IManager<PostComment>>(); }
            }

            public IManager<ContentBlock> ContentBlocks
            {
                get { return Resolver.GetInstance<IManager<ContentBlock>>(); }
            }

            public IManager<MailTemplate> MailTemplates
            {
                get { return Resolver.GetInstance<IManager<MailTemplate>>(); }
            }

            public IManager<WebsiteSetting> WebsiteSettings
            {
                get { return Resolver.GetInstance<IManager<WebsiteSetting>>(); }
            }

        }
    }
}
