using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Models.ViewModels
{
    public class LoginModel
    {
        public string login { get; set; }

        [DataType(DataType.Password)]
        public string password { get; set; }

        public LoginModel()
        {
            login = password = string.Empty;
        }
    }
}
