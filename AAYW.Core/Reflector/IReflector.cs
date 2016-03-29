using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Reflector
{
    public interface IReflector
    {
        IReflectionData Reflect<TType>();
        IReflectionData Reflect(Type type);
        IReflectionData Reflect(TypeInfo typeInfo);
        IReflectionData Reflect(string typeName);
        
        void DropCache();

        void DropCacheKey(Type key);
    }
}
