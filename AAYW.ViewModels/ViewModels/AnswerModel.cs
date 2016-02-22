using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.ViewModels
{
    public class AnswerModel
    {
        [Required]
        [AAYW.Core.Validation.MaxLength(1000, 0)]        
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public Guid ParentId { get; set; }

        public AnswerModel()
        {
            Description = string.Empty;
        }
    }
}
