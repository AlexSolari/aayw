using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Models
{
    public class Entity
    {
        public virtual Guid Id { get; set; }
        
        public virtual DateTime CreationTime { get; set; }
    }
}
