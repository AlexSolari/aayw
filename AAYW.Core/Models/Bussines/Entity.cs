using AAYW.Core.Annotations;
using sORM.Core;
using sORM.Core.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Models.Bussines
{
    public abstract class Entity
    {
        [MapAsType(sORM.Core.Mappings.DataType.String)]
        [InspectorLock]
        [Key]
        public virtual string Id { get; set; }

        [MapAsType(sORM.Core.Mappings.DataType.String)]
        public virtual DateTime CreatedDate { get; set; }

        [MapAsType(sORM.Core.Mappings.DataType.String)]
        public virtual DateTime ModifiedDate { get; set; }
    }
}
