using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Models.ViewModels
{
    public class AnswerModel
    {
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public Guid ParentId { get; set; }
    }
}
