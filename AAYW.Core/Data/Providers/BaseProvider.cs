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
        public BaseProvider()
        {

        }

        public virtual TEntity GetByField(string field, string value)
        {
            TEntity result = null;
            Execute(session =>
            {
                var criteria = session.CreateCriteria(typeof(TEntity));
                criteria.Add(Restrictions.Eq(field, value));
                result = criteria.UniqueResult<TEntity>();
            });
            return result;
        }

        public virtual IList<TEntity> GetListByField(string field, string value)
        {
            IList<TEntity> result = Resolver.GetInstance<IList<TEntity>>();
            Execute(session =>
            {
                var criteria = session.CreateCriteria(typeof(TEntity));
                criteria.Add(Restrictions.Eq(field, value));
                result = criteria.List<TEntity>();
            });
            return result;
        }

        public virtual IList<TEntity> GetList(int page = 0, int pagesize = 50)
        {
            return Execute(session =>
            {
                var criteria = session.CreateCriteria<TEntity>();
                criteria.AddOrder(Order.Desc("CreatedDate"));
                criteria.SetMaxResults(pagesize);
                criteria.SetFirstResult(pagesize * page);
                return criteria.List<TEntity>();
            });
        }

        public virtual IList<TEntity> All()
        {
            return Execute(session =>
            {
                var criteria = session.CreateCriteria<TEntity>();
                criteria.AddOrder(Order.Desc("CreatedDate"));
                return criteria.List<TEntity>();
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
            return Execute(session =>
            {
                return session.Get<TEntity>(id);
            });
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
                }
            });
        }
    }
}
