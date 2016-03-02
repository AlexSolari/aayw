using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Models.View.Settings
{
    public class MailSettings
    {
        public string MailAdress { get; set; }
        public string MailHost { get; set; }
        public int MailPort { get; set; }
        public string MailUsername { get; set; }
        [DataType(DataType.Password)]
        public string MailPassword { get; set; }
        public bool MailEnableSsl { get; set; }
    }
}
