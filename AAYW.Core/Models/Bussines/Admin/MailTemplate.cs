using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Models.Bussines.Admin
{
    public class MailTemplate : Entity
    {
        public virtual string Name { get; set; }
        public virtual string Body { get; set; }
    }
}
