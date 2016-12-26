using AAYW.Core.Models.Bussines;
using sORM.Core.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AAYW.Core.Models.Bussines.Admin
{
    [DataModel]
    public class UserForm : Entity
    {
        [MapAsType(sORM.Core.Mappings.DataType.String)]
        public virtual XmlDocument Fields { get; set; }

        [MapAsType(sORM.Core.Mappings.DataType.String)]
        public virtual string Header { get; set; }

        [MapAsType(sORM.Core.Mappings.DataType.String)]
        public virtual string Url { get; set; }

        [MapAsType(sORM.Core.Mappings.DataType.String)]
        [ReferenceTo(typeof(MailTemplate), "Name")]
        public virtual string MailTemplateName { get; set; }

        [MapAsType(sORM.Core.Mappings.DataType.String)]
        public virtual string SubmitAdress { get; set; }
    }
}
