using AAYW.Core.Models.Bussines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AAYW.Core.Models.Admin.Bussines
{
    public class UserForm : Entity
    {
        public virtual XmlDocument Fields { get; set; }
        public virtual string Header { get; set; }
        public virtual string Url { get; set; }
        public virtual string MailTemplateName { get; set; }
    }
}
