using AAYW.Core.Annotations;
using AAYW.Core.Models.Bussines;
using sORM.Core.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AAYW.Core.Models.Admin.Bussines
{
    [DataModel]
    public class WebsiteSetting : Entity
    {
        [CustomMaxLength(200)]
        [MapAsType(sORM.Core.Mappings.DataType.String)]
        public virtual string MailAdress { get; set; }
        [MapAsType(sORM.Core.Mappings.DataType.String)]
        [CustomMaxLength(200)]
        public virtual string MailHost { get; set; }
        [MapAsType(sORM.Core.Mappings.DataType.Int)]
        public virtual int MailPort { get; set; }
        [MapAsType(sORM.Core.Mappings.DataType.String)]
        [CustomMaxLength(200)]
        public virtual string MailUsername { get; set; }
        [MapAsType(sORM.Core.Mappings.DataType.String)]
        [CustomMaxLength(200)]
        public virtual string MailPassword { get; set; }
        [MapAsType(sORM.Core.Mappings.DataType.Bool)]
        public virtual bool MailEnableSsl { get; set; }
        [MapAsType(sORM.Core.Mappings.DataType.String)]
        public virtual XmlDocument CurrentTheme { get; set; }

        #region DataEntity members
        [MapAsType(sORM.Core.Mappings.DataType.String)]
        [InspectorLock]
        public override Guid Id { get; set; }

        [MapAsType(sORM.Core.Mappings.DataType.String)]
        public override DateTime CreatedDate { get; set; }

        [MapAsType(sORM.Core.Mappings.DataType.String)]
        public override DateTime ModifiedDate { get; set; }

        public override string DataId { get; set; }
        #endregion
    }
}
