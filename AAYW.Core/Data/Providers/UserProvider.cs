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
    public class UserProvider : DataProvider<User>
    {
        public UserProvider()
        {

        }

        public User GetByLogin(string login)
        {
            User result = null;
            Execute(session =>
            {
                var criteria = session.CreateCriteria(typeof(User));
                criteria.Add(Restrictions.Eq("Login", login));
                result = criteria.UniqueResult<User>();
            });
            return result;
        }
    }
}
