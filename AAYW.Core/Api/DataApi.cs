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

            public PostManager Posts
            {
                get { return (PostManager)Resolver.GetInstance<IManager<Post>>(); }
            }

            public UserManager Users
            {
                get { return (UserManager)Resolver.GetInstance<IManager<User>>(); }
            }

            public PageManager Pages
            {
                get { return (PageManager)Resolver.GetInstance<IManager<Page>>(); }
            }
            
            public UserFormManager UserForms
            {
                get { return (UserFormManager)Resolver.GetInstance<IManager<UserForm>>(); }
            }

            public PostCommentManager PostComments
            {
                get { return (PostCommentManager)Resolver.GetInstance<IManager<PostComment>>(); }
            }

            public ContentBlockManager ContentBlocks
            {
                get { return (ContentBlockManager)Resolver.GetInstance<IManager<ContentBlock>>(); }
            }

            public MailTemplateManager MailTemplates
            {
                get { return (MailTemplateManager)Resolver.GetInstance<IManager<MailTemplate>>(); }
            }

            public WebsiteSettingsManager WebsiteSettings
            {
                get { return (WebsiteSettingsManager)Resolver.GetInstance<IManager<WebsiteSetting>>(); }
            }

        }
    }
}
