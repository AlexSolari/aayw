using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace AAYW.Core.Reflector
{
    public class EntityReflectionData : IReflectionData
    {
        public IList<PropertyInfo> Properties { get; set; }

        public Type ReflectedType { get; set; }

        public EntityReflectionData(Type type)
        {
            ReflectedType = type;
            Properties = type.GetProperties();
        }
    }
}
