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
using AAYW.Core.Extensions;

namespace AAYW.Core.Web.Controller.Concrete
{
    public class PageController : FrontendController
    {
        IManager<Page> pagesManager = AAYW.Core.Dependecies.Resolver.GetInstance<IManager<Page>>();
        IManager<ContentBlock> contentBlocksManager = AAYW.Core.Dependecies.Resolver.GetInstance<IManager<ContentBlock>>();

        public override string Name
        {
            get { return "Pages"; }
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
