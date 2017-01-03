using AAYW.Core.Api;
using AAYW.Core.Dependecies;
using AAYW.Core.Models.Bussines;
using sORM.Core;
using sORM.Core.Conditions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Data.Providers
{
    public class BaseProvider<TEntity> : IProvider<TEntity>
        where TEntity : Entity
    {
        protected bool suppressLogging;

        public BaseProvider() : this(false)
        {
            
        }

        public BaseProvider(bool suppressLogging)
        {
            this.suppressLogging = suppressLogging;
        }

        protected void LogRequest(string method, Dictionary<string, string> parameters)
        {
            if (suppressLogging)
                return;

            var paramsString = new StringBuilder();

            foreach (var item in parameters)
            {
                paramsString.Append("{0} as [{1}], ".FormatWith(item.Key, item.Value));
            }

            SiteApi.Services.Logger.Log("Executing [{0}] in {1}Provider, with parameters: {2}".FormatWith(method, typeof(TEntity).Name, paramsString.ToString()));
        }

        protected TType WrapRequestExecution<TType>(Func<TType> action)
        {
            var perf = new Stopwatch();
            perf.Start();
            var result = action();
            perf.Stop();
            SiteApi.Services.Logger.Log(" >>> {0} ticks taken for executing request ({1} ms). | {2}".FormatWith(perf.ElapsedTicks, perf.ElapsedMilliseconds, action.Method.ToString()));
            return result;
        }

        public virtual TEntity GetByField(string field, object value)
        {
            LogRequest("GetByField", new Dictionary<string, string>() 
            { 
                { "field", field },
                { "value", value.ToString() },
            });

            return WrapRequestExecution(() => 
            {
                TEntity result = null;

                var key = "{0}-{1}-{2}/{3}".FormatWith(System.Reflection.MethodBase.GetCurrentMethod().Name, typeof(TEntity).Name, field, value);
                if ( SiteApi.Services.Cache.HasKey(key) )
                {
                    return SiteApi.Services.Cache.Get<TEntity>(key);
                }

                result = SimpleORM.Current.Get<TEntity>(Condition.Equals(field, value)).FirstOrDefault();

                SiteApi.Services.Cache.Add(result, key);
                return result;
            });
        }

        public virtual IList<TEntity> GetListByField(string field, object value)
        {
            LogRequest("GetListByField", new Dictionary<string, string>() 
            { 
                { "field", field },
                { "value", value.ToString() },
            });

            return WrapRequestExecution(() =>
            {
                IList<TEntity> result = Resolver.GetInstance<IList<TEntity>>();

                var key = "{0}-{1}-{2}/{3}".FormatWith(System.Reflection.MethodBase.GetCurrentMethod().Name, typeof(TEntity).Name, field, value);
                if (SiteApi.Services.Cache.HasKey(key))
                {
                    return SiteApi.Services.Cache.Get<IList<TEntity>>(key);
                }

                result = SimpleORM.Current.Get<TEntity>(Condition.Equals(field, value)).ToList();

                SiteApi.Services.Cache.Add(result, key);
                return result;
            });
        }

        public virtual IList<TEntity> GetList(int page = 0, int pagesize = 50)
        {
            LogRequest("GetList", new Dictionary<string, string>() 
            { 
                { "page", page.ToString() },
                { "pagesize", pagesize.ToString() },
            });

            return WrapRequestExecution(() =>
            {
                IList<TEntity> result = Resolver.GetInstance<IList<TEntity>>();

                var key = "{0}-{1}-{2}/{3}".FormatWith(System.Reflection.MethodBase.GetCurrentMethod().Name, typeof(TEntity).Name, page, pagesize);
                if (SiteApi.Services.Cache.HasKey(key))
                {
                    return SiteApi.Services.Cache.Get<IList<TEntity>>(key);
                }

                result = SimpleORM.Current.Get<TEntity>(options: new DataEntityListLoadOptions(pagesize, page)).ToList();

                SiteApi.Services.Cache.Add(result, key);
                return result;
            });
        }

        public virtual IList<TEntity> All()
        {
            LogRequest("All", new Dictionary<string, string>() { });

            return WrapRequestExecution(() =>
            {
                IList<TEntity> result = Resolver.GetInstance<IList<TEntity>>();

                var key = "{0}-{1}".FormatWith(System.Reflection.MethodBase.GetCurrentMethod().Name, typeof(TEntity).Name);
                if (SiteApi.Services.Cache.HasKey(key))
                {
                    return SiteApi.Services.Cache.Get<IList<TEntity>>(key);
                }

                result = SimpleORM.Current.Get<TEntity>(options: new DataEntityListLoadOptions(by: "CreatedDate")).ToList();

                SiteApi.Services.Cache.Add(result, key);
                return result;
            });
        }

        public TEntity GetById(string id)
        {
            Guid guid;
            if (Guid.TryParse(id, out guid))
            {
                return GetById(guid);
            }
            return null;
        }

        public TEntity GetById(Guid id)
        {
            LogRequest("GetById", new Dictionary<string, string>() 
            { 
                { "id", id.ToString() },
            });

            return WrapRequestExecution(() =>
            {
                TEntity result = null;

                var key = "{0}-{1}-{2}".FormatWith(System.Reflection.MethodBase.GetCurrentMethod().Name, typeof(TEntity).Name, id);
                if (SiteApi.Services.Cache.HasKey(key))
                {
                    return SiteApi.Services.Cache.Get<TEntity>(key);
                }

                result = SimpleORM.Current.Get<TEntity>(Condition.Equals("Id", id)).FirstOrDefault();

                SiteApi.Services.Cache.Add(result, key);
                return result;
            });
        }

        public void CreateOrUpdate(TEntity model)
        {
            LogRequest("CreateOrUpdate", new Dictionary<string, string>() 
            { 
                { "model", model.ToString() },
            });

            model.ModifiedDate = DateTime.Now;
            WrapRequestExecution(() =>
            {
                SimpleORM.Current.CreateOrUpdate(model);
                return 0;
            });
            SiteApi.Services.Cache.DropWhere(x => x.Contains("GetById") && x.Contains(model.Id.ToString()) && x.Contains(typeof(TEntity).Name));
            SiteApi.Services.Cache.DropWhere(x => x.Contains("All") && x.Contains(typeof(TEntity).Name));
            SiteApi.Services.Cache.DropWhere(x => x.Contains("List") && x.Contains(typeof(TEntity).Name));
            SiteApi.Services.Cache.DropWhere(x => x.Contains("GetByField") && x.Contains(typeof(TEntity).Name));
        }

        public void Delete(TEntity model)
        {
            LogRequest("Delete", new Dictionary<string, string>() 
            { 
                { "model", model.ToString() },
            });
            WrapRequestExecution(() =>
            {
                SimpleORM.Current.Delete(model);
                return 0;
            });
            SiteApi.Services.Cache.DropWhere(x => x.Contains("GetById") && x.Contains(model.Id.ToString()) && x.Contains(typeof(TEntity).Name));
            SiteApi.Services.Cache.DropWhere(x => x.Contains("All") && x.Contains(typeof(TEntity).Name));
            SiteApi.Services.Cache.DropWhere(x => x.Contains("List") && x.Contains(typeof(TEntity).Name));
            SiteApi.Services.Cache.DropWhere(x => x.Contains("GetByField") && x.Contains(typeof(TEntity).Name));
        }
    }
}
