using AAYW.Core.Annotations;
using sORM.Core.Mappings;
using AAYW.Core.Models.Bussines;
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
        [MapAsType(sORM.Core.Mappings.DataType.Guid)]
        [ReferenceTo(typeof(Post), "Id")]
        public virtual Guid PostId { get; set; }

        [InspectorLock]
        [MapAsType(sORM.Core.Mappings.DataType.Guid)]
        [ReferenceTo(typeof(User.User), "Id")]
        public virtual Guid UserId { get; set; }

        [CustomRequired("Content")]
        [CustomMaxLength(500)]
        [MapAsType(sORM.Core.Mappings.DataType.String)]
        public virtual string Content { get; set; }
    }
}
