using AAYW.Core.Models.Bussines.User;
using AAYW.Core.Models.Mappings;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Models.Mappings
{
    class UserMap : EntityMap<User>
    {
        public UserMap() : base()
        {
            Map(x => x.Login).Length(50);
            Map(x => x.PasswordHash);

            Table("Users");
        }
    }
}
