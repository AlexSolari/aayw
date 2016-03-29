using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace AAYW.Core.Reflector
{
    public interface IReflectionData
    {
        IList<PropertyInfo> Properties { get; set; }
        Type ReflectedType { get; set; }
    }
}
