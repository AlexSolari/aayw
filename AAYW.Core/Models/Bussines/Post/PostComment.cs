using AAYW.Core.Annotations;
using sORM.Core.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Models.Bussines.Post
{
    [DataModel]
    [Inspectable]
    public class PostComment : Entity
    {
        [InspectorLock]
        [MapAsType(sORM.Core.Mappings.DataType.String)]
        public virtual string PostId { get; set; }

        [InspectorLock]
        [MapAsType(sORM.Core.Mappings.DataType.String)]
        public virtual string UserId { get; set; }

        [CustomRequired("Content")]
        [CustomMaxLength(500)]
        [MapAsType(sORM.Core.Mappings.DataType.String)]
        public virtual string Content { get; set; }
    }
}
