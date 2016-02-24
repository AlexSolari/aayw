using AAYW.Core.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Models.View.User
{
    public class RegistrationModel
    {
        [CustomRequired]
        [CustomMaxLength(50,0)]
        public string Login { get; set; }

        [CustomRequired]
        [CustomMaxLength(50, 0)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [CustomRequired]
        [CustomMaxLength(50, 0)]
        [CustomCompare("Password")]
        [DataType(DataType.Password)]
        public string Confirmation { get; set; }

        public RegistrationModel()
        {
            Confirmation = Login = Password = string.Empty;
        }
    }
}
