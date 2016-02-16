using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Models
{
    public class Answer : Entity
    {
        public virtual string Description { get; set; }
        public virtual Guid QuestionId { get; set; }
    }
}
