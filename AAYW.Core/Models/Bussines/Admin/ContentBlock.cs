using AAYW.Core.Annotations;
using sORM.Core.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AAYW.Core.Models.Bussines.Admin
{
    [DataModel]
    public class ContentBlock : Entity
    {
        public sealed class BlockType
        {
            public const string Html = "html";
            public const string Feed = "feed";
            public const string Redirect = "redirect";
        }
        [MapAsType(sORM.Core.Mappings.DataType.String)]
        public virtual string Name { get; set; }
        [MapAsType(sORM.Core.Mappings.DataType.String)]
        public virtual string Type { get; set; }
        [MapAsType(sORM.Core.Mappings.DataType.String)]
        public virtual string Content { get; set; }

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
