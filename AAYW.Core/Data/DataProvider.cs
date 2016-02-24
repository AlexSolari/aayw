﻿using AAYW.Core.Models.Bussines;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;

namespace AAYW.Core.Data
{
    public class DataProvider<TEntity>
        where TEntity : Entity
    {
        protected ISession CreateSession()
        {
            return NHibernateHelper.OpenSession();
        }

        protected ITransaction StartTransaction(ISession session)
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
    }
}