using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Models.Bussines.Admin
{
    public class ContentBlock : Entity
    {
        public enum BlockType
        {
            Html,
        }

        public virtual BlockType Type { get; set; }
        public virtual string Content { get; set; }
    }
}
