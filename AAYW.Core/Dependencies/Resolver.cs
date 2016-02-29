using AAYW.Core.Web.Controller;
using AAYW.Core.Models.Bussines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace AAYW.Core.Dependecies
{
    public static class Resolver
    {
        static Dictionary<Type, Type> typeDependencies = new Dictionary<Type, Type>();
        static Dictionary<string, Type> controllerDependencies = new Dictionary<string, Type>();
        static Dictionary<Type, Type> entitiesDependencies = new Dictionary<Type, Type>();

        #region Types
        public static Type Resolve<T>()
        {
            if (typeDependencies.ContainsKey(typeof(T)))
                return typeDependencies[typeof(T)];
            else
                return typeof(T);
        }

        /// <typeparam name="T">What will be requested</typeparam>
        /// <typeparam name="I">What will be returned</typeparam>
        public static void RegisterType<T, I>()
            where I : T
        {
            typeDependencies.Add(typeof(T), typeof(I));
        }

        public static T GetInstance<T>()
        {
            var result = (T)Activator.CreateInstance(Resolve<T>());

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

        public static void RegisterEntity<T, I>()
            where I : T
        {
            typeDependencies.Add(typeof(T), typeof(I));
            entitiesDependencies.Add(typeof(T), typeof(I));
        }
        #endregion
    }
}
