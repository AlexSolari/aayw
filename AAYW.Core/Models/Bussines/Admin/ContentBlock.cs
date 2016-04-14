using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AAYW.Core.Models.Bussines.Admin
{
    public class ContentBlock : Entity
    {
        public enum BlockType
        {
            Html,
            Feed,
            Redirect
        }
        public virtual string Name { get; set; }
        public virtual BlockType Type { get; set; }
        public virtual string Content { get; set; }
    }
}
