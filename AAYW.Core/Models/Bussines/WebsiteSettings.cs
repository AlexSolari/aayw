using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Models.Bussines
{
    public class WebsiteSettings : Entity
    {
        public virtual string MailAdress { get; set; }
        public virtual string MailHost { get; set; }
        public virtual int MailPort { get; set; }
        public virtual string MailUsername { get; set; }
        public virtual string MailPassword { get; set; }
        public virtual bool MailEnableSsl { get; set; }
    }
}
