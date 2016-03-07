using AAYW.Core.Dependecies;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Map
{
    public class Mapper
    {
        public static Dictionary<Type, Func<object, object, object>> CustomMappings = new Dictionary<Type, Func<object, object, object>>();

        public static TDestination Map<TDestination, TSource>(TSource source)
        {
            var result = Resolver.GetInstance<TDestination>();

            foreach (var fieldR in typeof(TDestination).GetProperties())
            {
                foreach (var fieldS in typeof(TSource).GetProperties())
                {
                    if (fieldR.Name == fieldS.Name)
                    {
                        if (fieldR.PropertyType == typeof(Guid))
                        {
                            fieldR.SetValue(result, Guid.Parse((string)fieldS.GetValue(source)));
                        }
                        else if (fieldR.PropertyType == typeof(String))
                        {
                            fieldR.SetValue(result, fieldS.GetValue(source).ToString());
                        }
                        else
                        {
                            fieldR.SetValue(result, fieldS.GetValue(source));
                        }
                    }
                }
            }

            if (CustomMappings.ContainsKey(typeof(TSource)))
                result = (TDestination)CustomMappings[typeof(TSource)](result, source);

            return result;
        }
        public static TDestination MapAndMerge<TDestination, TSource>(TSource source, TDestination result)
        {
            foreach (var fieldR in typeof(TDestination).GetProperties())
            {
                foreach (var fieldS in typeof(TSource).GetProperties())
                {
                    if (fieldR.Name == fieldS.Name)
                    {
                        fieldR.SetValue(result, fieldS.GetValue(source));
                    }
                }
            }

            if (CustomMappings.ContainsKey(typeof(TSource)))
                result = (TDestination)CustomMappings[typeof(TSource)](result, source);

            return result;
        }

        public static TDestination MapAs<TDestination>(object source)
        {
            var result = Resolver.GetInstance<TDestination>();

            foreach (var fieldR in typeof(TDestination).GetProperties())
            {
                foreach (var fieldS in source.GetType().GetProperties())
                {
                    if (fieldR.Name == fieldS.Name)
                    {
                        fieldR.SetValue(result, fieldS.GetValue(source));
                    }
                }
            }

            if (CustomMappings.ContainsKey(source.GetType()))
                result = (TDestination)CustomMappings[source.GetType()](result, source);

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

        internal static dynamic Map(System.Reflection.TypeInfo entityType, Dictionary<string, string> modelData)
        {
            var result = Resolver.GetInstance(entityType);

            foreach (var fieldR in entityType.GetProperties())
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
                            TypeConverter typeConverter = TypeDescriptor.GetConverter(Type.GetType(type));
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
