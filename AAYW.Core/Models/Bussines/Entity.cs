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
    public abstract class Entity : DataEntity
    {
        public abstract Guid Id { get; set; }

        public abstract DateTime CreatedDate { get; set; }

        public abstract DateTime ModifiedDate { get; set; }

        public override string DataId { get; set; }
    }
}
