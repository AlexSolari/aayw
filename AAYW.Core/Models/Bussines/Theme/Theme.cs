using AAYW.Core.Annotations;
using sORM.Core;
using sORM.Core.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AAYW.Core.Models.Bussines.Theme
{
    [DataModel]
    public class Theme : Entity
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
    }
}
