using AAYW.Core.Cache;
using AAYW.Core.Dependecies;
using AAYW.Core.Models.Bussines;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Data.Providers
{
    public class BaseProvider<TEntity> : DataProvider<TEntity>, IProvider<TEntity>
        where TEntity : Entity
    {
        ICache cache;

        public BaseProvider()
        {
            cache = Resolver.GetInstance<ICache>();
        }

        public virtual TEntity GetByField(string field, string value)
        {
            TEntity result = null;

            var key = "{0}-{1}-{2}/{3}".FormatWith(System.Reflection.MethodBase.GetCurrentMethod().Name, typeof(TEntity).Name, field, value);
            if ( cache.HasKey(key) )
            {
                return cache.Get<TEntity>(key);
            }

            Execute(session =>
            {
                var criteria = session.CreateCriteria(typeof(TEntity));
                criteria.Add(Restrictions.Eq(field, value));
                result = criteria.UniqueResult<TEntity>();
            });

            cache.Add<TEntity>(result, key);
            return result;
        }

        public virtual IList<TEntity> GetListByField(string field, string value)
        {
            IList<TEntity> result = Resolver.GetInstance<IList<TEntity>>();

            var key = "{0}-{1}-{2}/{3}".FormatWith(System.Reflection.MethodBase.GetCurrentMethod().Name, typeof(TEntity).Name, field, value);
            if (cache.HasKey(key))
            {
                return cache.Get<IList<TEntity>>(key);
            }

            Execute(session =>
            {
                var criteria = session.CreateCriteria(typeof(TEntity));
                criteria.Add(Restrictions.Eq(field, value));
                result = criteria.List<TEntity>();
            });

            cache.Add<IList<TEntity>>(result, key);
            return result;
        }

        public virtual IList<TEntity> GetList(int page = 0, int pagesize = 50)
        {
            IList<TEntity> result = Resolver.GetInstance<IList<TEntity>>();

            var key = "{0}-{1}-{2}/{3}".FormatWith(System.Reflection.MethodBase.GetCurrentMethod().Name, typeof(TEntity).Name, page, pagesize);
            if (cache.HasKey(key))
            {
                return cache.Get<IList<TEntity>>(key);
            }

            Execute(session =>
            {
                var criteria = session.CreateCriteria<TEntity>();
                criteria.AddOrder(Order.Desc("CreatedDate"));
                criteria.SetMaxResults(pagesize);
                criteria.SetFirstResult(pagesize * page);
                result = criteria.List<TEntity>();
            });

            cache.Add<IList<TEntity>>(result, key);
            return result;
        }

        public virtual IList<TEntity> All()
        {
            IList<TEntity> result = Resolver.GetInstance<IList<TEntity>>();

            var key = "{0}-{1}".FormatWith(System.Reflection.MethodBase.GetCurrentMethod().Name, typeof(TEntity).Name);
            if (cache.HasKey(key))
            {
                return cache.Get<IList<TEntity>>(key);
            }

            Execute(session =>
            {
                var criteria = session.CreateCriteria<TEntity>();
                criteria.AddOrder(Order.Desc("CreatedDate"));
                result = criteria.List<TEntity>();
            });

            cache.Add<IList<TEntity>>(result, key);
            return result;
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
            TEntity result = null;

            var key = "{0}-{1}-{2}".FormatWith(System.Reflection.MethodBase.GetCurrentMethod().Name, typeof(TEntity).Name, id);
            if (cache.HasKey(key))
            {
                return cache.Get<TEntity>(key);
            }

            Execute(session =>
            {
                result = session.Get<TEntity>(id);
            });

            cache.Add<TEntity>(result, key);
            return result;
        }

        public void CreateOrUpdate(TEntity model)
        {
            Execute(session =>
            {
                using (var transaction = StartTransaction(session))
                {
                    model.ModifiedDate = DateTime.Now;
                    session.SaveOrUpdate(model);
                    transaction.Commit();

                    cache.DropWhere(x => x.Contains("GetById") && x.Contains(model.Id.ToString()) && x.Contains(typeof(TEntity).Name));
                    cache.DropWhere(x => x.Contains("All") && x.Contains(typeof(TEntity).Name));
                    cache.DropWhere(x => x.Contains("List") && x.Contains(typeof(TEntity).Name));
                    cache.DropWhere(x => x.Contains("GetByField") && x.Contains(typeof(TEntity).Name));
                }
            });
        }

        public void Delete(TEntity model)
        {
            Execute(session =>
            {
                using (var transaction = StartTransaction(session))
                {
                    session.Delete(model);
                    session.Flush();
                    transaction.Commit();

                    cache.DropWhere(x => x.Contains("GetById") && x.Contains(model.Id.ToString()) && x.Contains(typeof(TEntity).Name));
                    cache.DropWhere(x => x.Contains("All") && x.Contains(typeof(TEntity).Name));
                    cache.DropWhere(x => x.Contains("List") && x.Contains(typeof(TEntity).Name));
                    cache.DropWhere(x => x.Contains("GetByField") && x.Contains(typeof(TEntity).Name));
                }
            });
        }
    }
}
