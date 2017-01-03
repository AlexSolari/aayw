using AAYW.Core.Data.Managers;
using AAYW.Core.Mail;
using AAYW.Core.Map;
using AAYW.Core.Models.Bussines.Admin;
using AAYW.Core.Models.View.UserForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AAYW.Core.Models.Bussines.Post;
using AAYW.Core.Api;

namespace AAYW.Core.Web.Controller.Concrete
{
    public class PageController : FrontendController
    {
        public override string Name
        {
            get { return "Pages"; }
        }

        [HttpGet]
        public ActionResult Post(string id)
        {
            var post = SiteApi.Data.Posts.GetById(id);
            
            if (post == null)
            {
                return RedirectToRoute("Error404");
            }

            return View(post);
        }

        [HttpPost]
        public ActionResult AddPostComment(string PostId, string UserId, string Content)
        {
            if (Content.IsNullOrWhiteSpace() || Content.Length > 500)
            {
                return RedirectToAction("Post", new { id = PostId });
            }

            var comment = Dependecies.Resolver.GetInstance<PostComment>();
            comment.UserId = Guid.Parse(UserId);
            comment.PostId = Guid.Parse(PostId);
            comment.Content = Content;

            SiteApi.Data.PostComments.CreateOrUpdate(comment);

            return RedirectToAction("Post", new { id = PostId });
        }

        [HttpGet]
        public ActionResult Page(string url)
        {
            var page = SiteApi.Data.Pages.GetByField("Url", url);

            if (page == null)
            {
                return RedirectToRoute("Error404");
            }
            ViewBag.Title = page.Title;

            var blockIds = page.ContentBlocks.DeserializeAs<List<string>>();
            var blocks = new List<ContentBlock>();

            foreach (var id in blockIds)
            {
                var block = SiteApi.Data.ContentBlocks.GetById(id);

                if (block.Type == ContentBlock.BlockType.Redirect)
                {
                    return RedirectPermanent(block.Content.Trim());
                }

                blocks.Add(block);
            }

            return View(blocks);
        }
    }
}
