using AAYW.Core.Dependecies;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AAYW.Core.Reflector;
using AAYW.Core.Logging;

namespace AAYW.Core.Map
{
    public class Mapper
    {
        public static Dictionary<Type, Func<object, object, object>> CustomMappings = new Dictionary<Type, Func<object, object, object>>();

        public static TDestination Map<TDestination, TSource>(TSource source)
        {
            var result = Resolver.GetInstance<TDestination>();
            var reflector = Resolver.GetInstance<IReflector>();
            var sourceReflection = reflector.Reflect(typeof(TSource));
            var destinationReflection = reflector.Reflect(typeof(TDestination));

            foreach (var fieldR in destinationReflection.Properties)
            {
                foreach (var fieldS in sourceReflection.Properties)
                {
                    if (fieldR.Name == fieldS.Name)
                    {
                        if (fieldR.PropertyType == fieldS.PropertyType)
                        {
                            fieldR.SetValue(result, fieldS.GetValue(source));
                        }
                        else if (fieldR.PropertyType == typeof(Guid) && fieldS.PropertyType == typeof(string))
                        {
                            fieldR.SetValue(result, Guid.Parse((string)fieldS.GetValue(source)));
                        }
                        else if (fieldR.PropertyType == typeof(String))
                        {
                            fieldR.SetValue(result, fieldS.GetValue(source).SafeToString());
                        }
                        else
                        {
                            try
                            {

                                fieldR.SetValue(result, Convert.ChangeType(fieldS.GetValue(source), fieldR.PropertyType));
                            }
                            catch (InvalidCastException exc)
                            {
                                Resolver.GetInstance<ILogger>().Log("Error while mapping. Details: {0}".FormatWith(exc.Message));
                            }
                        }
                    }
                }
            }

            if (CustomMappings.ContainsKey(sourceReflection.ReflectedType))
                result = (TDestination)CustomMappings[sourceReflection.ReflectedType](result, source);

            return result;
        }
        public static TDestination MapAndMerge<TDestination, TSource>(TSource source, TDestination result)
        {
            var reflector = Resolver.GetInstance<IReflector>();
            var sourceReflection = reflector.Reflect(typeof(TSource));
            var destinationReflection = reflector.Reflect(typeof(TDestination));

            foreach (var fieldR in destinationReflection.Properties)
            {
                foreach (var fieldS in sourceReflection.Properties)
                {
                    if (fieldR.Name == fieldS.Name)
                    {
                        fieldR.SetValue(result, fieldS.GetValue(source));
                    }
                }
            }

            if (CustomMappings.ContainsKey(sourceReflection.ReflectedType))
                result = (TDestination)CustomMappings[sourceReflection.ReflectedType](result, source);

            return result;
        }

        public static TDestination MapAs<TDestination>(object source)
        {
            var result = Resolver.GetInstance<TDestination>();
            var reflector = Resolver.GetInstance<IReflector>();
            var sourceReflection = reflector.Reflect(source.GetType());
            var destinationReflection = reflector.Reflect(typeof(TDestination));

            foreach (var fieldR in destinationReflection.Properties)
            {
                foreach (var fieldS in sourceReflection.Properties)
                {
                    if (fieldR.Name == fieldS.Name)
                    {
                        fieldR.SetValue(result, fieldS.GetValue(source));
                    }
                }
            }

            if (CustomMappings.ContainsKey(sourceReflection.ReflectedType))
                result = (TDestination)CustomMappings[sourceReflection.ReflectedType](result, source);

            return result;
        }

        public static IEnumerable<TDestination> Map<TDestination, TSource>(IEnumerable<TSource> source)
        {
            var result = new List<TDestination>();
            foreach (var item in source)
            {
                result.Add(Map<TDestination, TSource>(item));
            }
            return result;
        }

        public static void AddMapping<TDestination, TSource>(Func<TDestination, TSource, TDestination> mapping)
        {
            if (CustomMappings.ContainsKey(typeof(TSource)))
                throw new ArgumentException("Mapping for this type already created");

            var tmp = Wrapper(mapping);

            CustomMappings.Add(typeof(TSource), tmp);
        }

        static Func<object, object, object> Wrapper<T, I>(Func<T, I, T> mapping)
        {
            return (r, s) =>
            {
                var R = (T)r;
                var S = (I)s;
                return mapping(R, S);
            };
        }

        internal static dynamic Map(Type entityType, Dictionary<string, string> modelData)
        {
            var result = Resolver.GetInstance(entityType);

            foreach (var fieldR in Resolver.GetInstance<IReflector>().Reflect(entityType).Properties)
            {
                foreach (var fieldS in modelData)
                {
                    var type = fieldS.Key.Split('@').Last();
                    var name = fieldS.Key.Split('@').First();
                    if (fieldR.Name == name)
                    {
                        if (fieldR.PropertyType.IsEnum)
                        {
                            fieldR.SetValue(result, Enum.Parse(fieldR.PropertyType, fieldS.Value));
                        }
                        else
                        {
                            TypeConverter typeConverter = TypeDescriptor.GetConverter(Resolver.GetInstance<IReflector>().Reflect(type).ReflectedType);
                            object propValue = typeConverter.ConvertFromString(fieldS.Value);

                            fieldR.SetValue(result, propValue);
                        }
                    }
                }
            }

            return result;
        }
    }
}
