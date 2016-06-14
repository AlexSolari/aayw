using AAYW.Core.Models.Bussines;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAYW.Core.Models.Bussines.User;

namespace AAYW.Core.Data.Providers
{
    public class UserProvider : BaseProvider<User>
    {
        public UserProvider() : base() { }
        public UserProvider(bool suppressLogging) : base(suppressLogging) { }

        public User GetByLogin(string login)
        {
            return GetByField("Login", login);
        }
    }
}
