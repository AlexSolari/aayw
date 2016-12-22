using AAYW.Core.Annotations;
using AAYW.Core.Models;
using sORM.Core.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Models.Bussines.User
{
    [Inspectable]
    [DataModel]
    public class User : Entity
    {
        public sealed class Role
        {
            public const string User = "user";
            public const string Admin = "admin";
        }

        [MapAsType(DataType.String)]
        public virtual string Login { get; set; }

        [MapAsType(DataType.String)]
        public virtual string PasswordHash { get; set; }

        [MapAsType(DataType.String)]
        public virtual string CurrentRole { get; set; }
    }
}
