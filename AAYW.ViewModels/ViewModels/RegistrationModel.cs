using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.ViewModels
{
    public class RegistrationModel
    {
        [Required]
        [AAYW.Core.Validation.MaxLength(50,0)]
        public string login { get; set; }

        [Required]
        [AAYW.Core.Validation.MaxLength(50, 0)]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required]
        [AAYW.Core.Validation.MaxLength(50, 0)]
        [DataType(DataType.Password)]
        public string confirmation { get; set; }

        public RegistrationModel()
        {
            confirmation = login = password = string.Empty;
        }
    }
}
