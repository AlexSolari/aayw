using AAYW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.DependecyResolver
{
    public static class Resolver
    {
        static Dictionary<Type, Type> typeDependencies = new Dictionary<Type, Type>();

        private static Type Resolve<T>()
        {
            if (typeDependencies.ContainsKey(typeof(T)))
                return typeDependencies[typeof(T)];
            else
                return typeof(T);
        }

        public static void RegisterType<T, I>()
        {
            typeDependencies.Add(typeof(T), typeof(I));
        }

        public static T GetInstance<T>()
        {
            var result = (T)Activator.CreateInstance(Resolve<T>());

            if (result is Entity)
            {
                (result as Entity).CreationTime = DateTime.Now;
                (result as Entity).Id = Guid.NewGuid();
            }

            return result;
        }
    }
}
