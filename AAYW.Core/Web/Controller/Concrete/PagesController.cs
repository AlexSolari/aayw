﻿using AAYW.Core.Data.Managers;
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

namespace AAYW.Core.Web.Controller.Concrete
{
    public class PageController : FrontendController
    {
        IManager<Page> pagesManager = AAYW.Core.Dependecies.Resolver.GetInstance<IManager<Page>>();
        IManager<ContentBlock> contentBlocksManager = AAYW.Core.Dependecies.Resolver.GetInstance<IManager<ContentBlock>>();
        IManager<Post> postsManager = AAYW.Core.Dependecies.Resolver.GetInstance<IManager<Post>>();
        IManager<PostComment> commentsManager = AAYW.Core.Dependecies.Resolver.GetInstance<IManager<PostComment>>();

        public override string Name
        {
            get { return "Pages"; }
        }

        [HttpGet]
        public ActionResult Post(string id)
        {
            var post = postsManager.GetById(id);
            
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
            comment.UserId = UserId;
            comment.PostId = PostId;
            comment.Content = Content;

            commentsManager.CreateOrUpdate(comment);

            return RedirectToAction("Post", new { id = PostId });
        }

        [HttpGet]
        public ActionResult Page(string url)
        {
            var page = pagesManager.GetByField("Url", url);

            if (page == null)
            {
                return RedirectToRoute("Error404");
            }
            ViewBag.Title = page.Title;

            var blockIds = page.ContentBlocks.DeserializeAs<List<string>>();
            var blocks = new List<ContentBlock>();

            foreach (var id in blockIds)
            {
                var block = contentBlocksManager.GetById(id);

                if (block.Type == ContentBlock.BlockType.Redirect)
                {
                    return RedirectToRoute("CustomPage", new { url = block.Content });
                }

                blocks.Add(block);
            }

            return View(blocks);
        }
    }
}
