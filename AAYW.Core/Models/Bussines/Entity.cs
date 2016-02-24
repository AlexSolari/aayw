using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Models.Bussines
{
    public class Entity
    {
        public virtual Guid Id { get; set; }
        
        public virtual DateTime CreatedDate { get; set; }

        public virtual DateTime ModifiedDate { get; set; }
    }
}
