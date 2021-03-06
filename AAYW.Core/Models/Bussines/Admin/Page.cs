﻿using AAYW.Core.Annotations;
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
    public class Page : Entity
    {
        [MapAsType(sORM.Core.Mappings.DataType.String)]
        public virtual string Url { get; set; }

        [MapAsType(sORM.Core.Mappings.DataType.String)]
        public virtual string Title { get; set; }

        [MapAsType(sORM.Core.Mappings.DataType.String)]
        public virtual XmlDocument ContentBlocks { get; set; }
    }
}
