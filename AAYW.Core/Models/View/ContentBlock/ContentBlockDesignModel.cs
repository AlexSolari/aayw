using AAYW.Core.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AAYW.Core.Models.View.ContentBlock
{
    public class ContentBlockDesignModel
    {
        public string Id { get; set; }
        public AAYW.Core.Models.Bussines.Admin.ContentBlock.BlockType Type { get; set; }
        public string Name { get; set; }
        [UIHint("HtmlEditor")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Content { get; set; }

        public ContentBlockDesignModel()
        {

        }

        public ContentBlockDesignModel(Guid id)
            : this()
        {
            Id = id.ToString();
        }
    }
}
