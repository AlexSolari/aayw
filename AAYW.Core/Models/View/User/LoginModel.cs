using System;
using System.Collections.Generic;
using AAYW.Core.Annotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AAYW.Core.Models.View.User
{
    public class LoginModel
    {
        [CustomRequired]
        [CustomMaxLength(50, 0)]
        public string Login { get; set; }

        [CustomRequired]
        [CustomMaxLength(50, 0)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public LoginModel()
        {
            Login = Password = string.Empty;
        }
    }
}
