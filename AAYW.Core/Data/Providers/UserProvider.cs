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
    public class UserProvider : BaseProvider<User>
    {
        public UserProvider()
        {

        }

        public User GetByLogin(string login)
        {
            return GetByField("Login", login);
        }
    }
}
