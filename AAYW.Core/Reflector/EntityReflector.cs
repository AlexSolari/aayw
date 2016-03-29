using AAYW.Core.Dependecies;
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

            return Reflect((Type)Assembly.GetExecutingAssembly().DefinedTypes.Where(x => x.Name.Equals(typeName)).FirstOrDefault());
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
