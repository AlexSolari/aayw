using AAYW.Core.Dependecies;
using AAYW.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Reflector
{
    public class EntityReflector : IReflector
    {
        static Dictionary<Type, IReflectionData> reflectionCache { get; set; }

        static EntityReflector()
        {
            Resolver.GetInstance<ILogger>().Log("Reflecting and caching all assembled classes");
            reflectionCache = new Dictionary<Type, IReflectionData>();
            var types = Assembly.GetExecutingAssembly().DefinedTypes;
            foreach (var type in types)
            {
                if (reflectionCache.ContainsKey(type))
                    continue;

                var reflectionData = Resolver.GetInstance<IReflectionData>(type);

                reflectionCache.Add(type, reflectionData);
            }
        }

        public EntityReflector()
        {
            
        }

        public IReflectionData Reflect<TType>()
        {
            return Reflect(typeof(TType));
        }

        public IReflectionData Reflect(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("Type can not be null");

            if (reflectionCache.ContainsKey(type))
            {
                return reflectionCache[type];
            }
            
            var reflectionData = Resolver.GetInstance<IReflectionData>(type);

            reflectionCache.Add(type, reflectionData);

            return reflectionData;
        }

        public IReflectionData Reflect(TypeInfo typeInfo)
        {
            return Reflect((Type)typeInfo);
        }

        public IReflectionData Reflect(string typeName)
        {
            foreach (var type in reflectionCache)
            {
                if (type.Key.Name.Equals(typeName))
                {
                    return type.Value;
                }
            }

            Type typeToReflect = 
                Assembly.GetExecutingAssembly().DefinedTypes
                .Where(x => x.Name.Equals(typeName))
                .FirstOrDefault() ?? Type.GetType(typeName);

            return Reflect(typeToReflect);
        }

        public void DropCache()
        {
            reflectionCache.Clear();
        }

        public void DropCacheKey(Type key)
        {
            reflectionCache.Remove(key);
        }
    }
}
