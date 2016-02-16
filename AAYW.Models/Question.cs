using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Models
{
    public class Question : Entity
    {
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual int Views { get; set; }
        public virtual Guid AutorId { get; set;}
    }
}
