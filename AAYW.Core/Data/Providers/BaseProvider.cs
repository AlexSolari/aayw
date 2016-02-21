using AAYW.Core.Dependecies;
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

        public T GetByField(string field, string value)
        {
            T result = null;
            Execute(session =>
            {
                var criteria = session.CreateCriteria(typeof(T));
                criteria.Add(Restrictions.Eq(field, value));
                result = criteria.UniqueResult<T>();
            });
            return result;
        }

        public IList<T> GetListByField(string field, string value)
        {
            IList<T> result = Resolver.GetInstance<IList<T>>();
            Execute(session =>
            {
                var criteria = session.CreateCriteria(typeof(T));
                criteria.Add(Restrictions.Eq(field, value));
                result = criteria.List<T>();
            });
            return result;
        }
    }
}
