using sORM.Core;
using sORM.Core.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Models.Bussines.Theme
{
    [DataModel]
    public class Theme : DataEntity
    {
        [UIHint("Color")]
        [MapAsType(sORM.Core.Mappings.DataType.String)]
        public string PrimaryColor { get; set; }
        [UIHint("Color")]
        [MapAsType(sORM.Core.Mappings.DataType.String)]
        public string PrimaryText { get; set; }

        [UIHint("Color")]
        [MapAsType(sORM.Core.Mappings.DataType.String)]
        public string DarkPrimary { get; set; }
        [UIHint("Color")]
        [MapAsType(sORM.Core.Mappings.DataType.String)]
        public string DarkPrimaryText { get; set; }

        [UIHint("Color")]
        [MapAsType(sORM.Core.Mappings.DataType.String)]
        public string Divider { get; set; }

        [UIHint("Color")]
        [MapAsType(sORM.Core.Mappings.DataType.String)]
        public string SecondaryText { get; set; }

        [UIHint("Color")]
        [MapAsType(sORM.Core.Mappings.DataType.String)]
        public string Danger { get; set; }
        [UIHint("Color")]
        [MapAsType(sORM.Core.Mappings.DataType.String)]
        public string DangerPrimary { get; set; }
        [UIHint("Color")]
        [MapAsType(sORM.Core.Mappings.DataType.String)]
        public string DangerAccent { get; set; }

        [UIHint("Color")]
        [MapAsType(sORM.Core.Mappings.DataType.String)]
        public string Accent { get; set; }

        [ScaffoldColumn(false)]
        public override string DataId { get; set; }
    }
}
