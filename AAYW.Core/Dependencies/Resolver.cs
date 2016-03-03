using AAYW.Core.Web.Controller;
using AAYW.Core.Models.Bussines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using AAYW.Core.Annotations;

namespace AAYW.Core.Dependecies
{
    public static class Resolver
    {
        static Dictionary<Type, Type> typeDependencies = new Dictionary<Type, Type>();
        static Dictionary<string, Type> controllerDependencies = new Dictionary<string, Type>();
        static Dictionary<Type, Type> entitiesDependencies = new Dictionary<Type, Type>();
        static Dictionary<Type, Type> managerDependencies = new Dictionary<Type, Type>();

        #region Types
        public static Type Resolve<T>()
        {
            if (typeDependencies.ContainsKey(typeof(T)))
                return typeDependencies[typeof(T)];
            else
                return typeof(T);
        }

        public static Type Resolve(Type type)
        {
            if (typeDependencies.ContainsKey(type))
                return typeDependencies[type];
            else
                return type;
        }

        /// <typeparam name="T">What will be requested</typeparam>
        /// <typeparam name="I">What will be returned</typeparam>
        public static void RegisterType<T, I>()
            where I : T
        {
            typeDependencies.Add(typeof(T), typeof(I));
            if (typeof(I).GetCustomAttributes(typeof(InspectableAttribute), false).Length > 0)
            {
                entitiesDependencies.Add(typeof(T), typeof(I));
            }
            if (typeof(I).GetCustomAttributes(typeof(ManagerForAttribute), false).Length > 0)
            {
                managerDependencies.Add(
                    ((ManagerForAttribute)(typeof(I)
                    .GetCustomAttributes(typeof(ManagerForAttribute), false)
                    .FirstOrDefault())).entityType, typeof(I));
            }
        }

        public static T GetInstance<T>(params object[] args)
        {
            var result = (T)Activator.CreateInstance(Resolve<T>(), args);

            if (result is Entity)
            {
                var entity = result as Entity;
                entity.CreatedDate = DateTime.Now;
                entity.ModifiedDate = DateTime.Now;
                entity.Id = Guid.NewGuid();
            }

            return result;
        }

        public static object GetInstance(Type type, params object[] args)
        {
            var result = Activator.CreateInstance(Resolve(type), args);

            if (result is Entity)
            {
                var entity = result as Entity;
                entity.CreatedDate = DateTime.Now;
                entity.ModifiedDate = DateTime.Now;
                entity.Id = Guid.NewGuid();
            }

            return result;
        }
        #endregion

        #region Controllers
        
        /// <typeparam name="T">What will be requested</typeparam>
        /// <typeparam name="I">What will be returned</typeparam>
        public static void RegisterController<T, I>(string name)
            where I : T
            where T : FrontendController
        {
            controllerDependencies.Add(name, typeof(I));
        }

        public static FrontendController GetController(string controllerName)
        {
            if (!controllerDependencies.ContainsKey(controllerName))
            {
                throw new KeyNotFoundException("Controller with given name not found");
            }

            var result = (FrontendController)Activator.CreateInstance(controllerDependencies[controllerName]);

            return result;
        }

        public static IList<FrontendController> GetAllControllers()
        {
            var result = new List<FrontendController>();

            foreach (var item in controllerDependencies)
            {
                result.Add(GetController(item.Key));   
            }

            return result;
        }
        #endregion

        #region Entities
        public static Dictionary<Type, Type> Entites
        {
            get
            {
                return entitiesDependencies;
            }
            private set { }
        }
        public static Dictionary<Type, Type> Managers
        {
            get
            {
                return managerDependencies;
            }
            private set { }
        }
        #endregion

        #region Routes

        public static Dictionary<string, string> RouteUrl = new Dictionary<string, string>();

        #endregion
    }
}
