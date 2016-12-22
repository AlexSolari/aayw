using AAYW.Core.Annotations;
using sORM.Core.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Models.Bussines.Admin
{
    [DataModel]
    public class MailTemplate : Entity
    {
        [MapAsType(sORM.Core.Mappings.DataType.String)]
        public virtual string Name { get; set; }

        [MapAsType(sORM.Core.Mappings.DataType.String)]
        public virtual string Body { get; set; }
    }
}
