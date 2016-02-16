using AAYW.Models;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;

namespace AAYW.Database
{
    public class DataProvider<TEntity>
        where TEntity : Entity
    {
        private ISession CreateSession()
        {
            return NHibernateHelper.OpenSession();
        }

        private ITransaction StartTransaction(ISession session)
        {
            return session.BeginTransaction();
        }

        protected T Execute<T>(Func<ISession, T> func, string errorMessage = null)
        {
            try
            {
                using (var session = CreateSession())
                {
                    return func(session);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void Execute(Action<ISession> action, string errorMessage = null)
        {
            try
            {
                using (var session = CreateSession())
                {
                    action(session);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual IList<TEntity> GetList()
        {
            return Execute(session =>
            {
                var criteria = session.CreateCriteria<TEntity>();
                criteria.AddOrder(Order.Desc("CreationTime"));
                return criteria.List<TEntity>();
            });
        }

        public TEntity GetById(string id)
        {
            return GetById((string.IsNullOrWhiteSpace(id)) ? Guid.Empty : Guid.Parse(id));
        }

        public TEntity GetById(Guid id)
        {
            return Execute(session =>
            {
                return session.Get<TEntity>(id);
            });
        }

        public void Create(TEntity model)
        {
            Execute(session =>
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(model);
                    transaction.Commit();
                }
            });
        }

        public void Update(TEntity model)
        {
            Execute(session =>
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(model);
                    session.Flush();
                    transaction.Commit();
                }
            });
        }

        public void Delete(TEntity model)
        {
            Execute(session =>
            {
                session.Delete(model);
                session.Flush();
            });
        }
    }
}