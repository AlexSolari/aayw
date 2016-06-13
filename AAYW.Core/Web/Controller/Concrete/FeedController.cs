using AAYW.Core.Models.Bussines.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AAYW.Core.Data.Managers;
using AAYW.Core.Api;
using AAYW.Core.Annotations;

namespace AAYW.Core.Web.Controller.Concrete
{
    [AccessLevel(Models.Bussines.User.User.Role.Admin)]
    public class FeedController : FrontendController
    {
        IManager<Post> postManager = SiteApi.Data.Posts;

        public FeedController()
        {

        }

        public override string Name
        {
            get
            {
                return "Feed";
            }
        }

        [HttpGet]
        public ActionResult CreatePost(string feedId)
        {
            var post = AAYW.Core.Dependecies.Resolver.GetInstance<Post>();

            post.FeedId = feedId;

            return PartialView("PostEditor", post);
        }

        [HttpGet]
        public ActionResult EditPost(string postId)
        {
            var post = postManager.GetById(postId);

            return PartialView("PostEditor", post);
        }

        [HttpPost]
        public ActionResult CreateOrUpdatePost(Post post, string returnUrl)
        {
            if (ModelState.IsValid)
                postManager.CreateOrUpdate(post);
            else
            {
                return Json(ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { key = x.Key, errors = x.Value.Errors }));
            }

            return Json(returnUrl);
        }

        [HttpPost]
        public ActionResult DeletePost(string id)
        {
            var postToDelete = postManager.GetById(id);

            if (postToDelete != null)
            {
                postManager.Delete(postToDelete);
            }

            return Json(true);
        }
    }
}
