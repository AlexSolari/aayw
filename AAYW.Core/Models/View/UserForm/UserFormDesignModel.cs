using AAYW.Core.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Models.View.UserForm
{
    public class UserFormDesignModel
    {
        public List<UserFormField> Fields { get; set; }
        [CustomRequired]
        public string Header { get; set; }
        [CustomRequired]
        public string Url { get; set; }
        [CustomRequired]
        public string MailTemplateName { get; set; }

        public UserFormDesignModel()
        {
            Fields = new List<UserFormField>();
        }
    }
}
