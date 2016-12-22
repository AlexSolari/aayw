using AAYW.Core.Annotations;
using sORM.Core.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AAYW.Core.Models.Bussines.Post
{
    [DataModel]
    [Inspectable]
    public class Post : Entity
    {
        [CustomRequired("Title")]
        [CustomMaxLength(500)]
        [MapAsType(sORM.Core.Mappings.DataType.String)]
        public virtual string Title { get; set; }

        [CustomRequired("Content")]
        [UIHint("HtmlEditor")]
        [CustomMaxLength(2000, PlainTextOnly = true)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
        [MapAsType(sORM.Core.Mappings.DataType.String)]
        [AllowHtml]
        public virtual string Content { get; set; }

        [MapAsType(sORM.Core.Mappings.DataType.String)]
        [UIHint("FeedSelector")]
        public virtual string FeedId { get; set; }
    }
}
