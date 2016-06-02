using AAYW.Core.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AAYW.Core.Models.Bussines.Post
{
    [Inspectable]
    public class Post : Entity
    {
        [CustomRequired("Title")]
        [CustomMaxLength(500)]
        public virtual string Title { get; set; }
        [CustomRequired("Content")]
        [UIHint("HtmlEditor")]
        [CustomMaxLength(2000, PlainTextOnly = true)]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public virtual string Content { get; set; }
        [UIHint("FeedSelector")]
        public virtual string FeedId { get; set; }
    }
}
