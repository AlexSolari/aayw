using AAYW.Core.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Models.Bussines.Post
{
    [Inspectable]
    public class PostComment : Entity
    {
        [InspectorLock]
        public virtual string PostId { get; set; }

        [InspectorLock]
        public virtual string UserId { get; set; }

        [CustomRequired("Content")]
        [CustomMaxLength(500)]
        public virtual string Content { get; set; }
    }
}
