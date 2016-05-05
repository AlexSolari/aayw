using AAYW.Core.Annotations;
using AAYW.Core.Models.Bussines;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Models.Admin.Bussines
{
    public class WebsiteSetting : Entity
    {
        [CustomMaxLength(200)]
        public virtual string MailAdress { get; set; }
        [CustomMaxLength(200)]
        public virtual string MailHost { get; set; }
        public virtual int MailPort { get; set; }
        [CustomMaxLength(200)]
        public virtual string MailUsername { get; set; }
        [CustomMaxLength(200)]
        public virtual string MailPassword { get; set; }
        public virtual bool MailEnableSsl { get; set; }
    }
}
