using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Models.View.User
{
    public class LoginModel
    {
        [Required]
        [AAYW.Core.Validation.MaxLength(50, 0)]
        public string login { get; set; }

        [Required]
        [AAYW.Core.Validation.MaxLength(50, 0)]
        [DataType(DataType.Password)]
        public string password { get; set; }

        public LoginModel()
        {
            login = password = string.Empty;
        }
    }
}
