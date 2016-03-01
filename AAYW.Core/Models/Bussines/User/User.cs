using AAYW.Core.Annotations;
using AAYW.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Models.Bussines.User
{
    [Inspectable]
    public class User : Entity
    {
        public enum Role : int
        {
            User,
            Admin
        }
        public virtual string Login { get; set; }
        public virtual string PasswordHash { get; set; }
        public virtual Role CurrentRole { get; set; }
    }
}
