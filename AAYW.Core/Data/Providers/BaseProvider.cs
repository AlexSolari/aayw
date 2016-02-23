using AAYW.Database;
using AAYW.Models;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Data.Providers
{
    public class BaseProvider<T> : DataProvider<T>
        where T : Entity
    {
        public BaseProvider()
        {

        }

        public T GetByField(string field, string login)
        {
            T result = null;
            Execute(session =>
            {
                var criteria = session.CreateCriteria(typeof(T));
                criteria.Add(Restrictions.Eq(field, login));
                result = criteria.UniqueResult<T>();
            });
            return result;
        }
    }
}
