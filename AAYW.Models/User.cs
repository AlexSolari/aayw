using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Models
{
    public class User : Entity
    {
        public virtual string Login { get; set; }
        public virtual string PasswordHash { get; set; }
    }
}
