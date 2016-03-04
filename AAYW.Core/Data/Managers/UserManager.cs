using AAYW.Core.Data.Providers;
using AAYW.Core.Dependecies;
using AAYW.Core.Models.Bussines;
using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using AAYW.Core.Extensions;
using AAYW.Core.Crypto;
using AAYW.Core.Models.Bussines.User;
using System.Security.Cryptography;
using AAYW.Core.Annotations;

namespace AAYW.Core.Data.Managers
{
    [ManagerFor(typeof(User))]
    public class UserManager : BaseManager<UserProvider, User>
    {
        public UserManager()
        {
            if (CurrentUser == null && IsAuthorized)
            {
                Logout();
            }
        }

        public bool Register(string login, string passwordRaw)
        {
            if (provider.GetByLogin(login) != null)
                return false;

            var newuser = Resolver.GetInstance<User>();

            newuser.PasswordHash = Resolver.GetInstance<ICryptoProcessor<MD5>>().CryptPassword(newuser, passwordRaw);
            newuser.Login = login;
            newuser.CurrentRole = User.Role.User;

            provider.CreateOrUpdate(newuser);

            return true;
        }

        public bool Login(string login, string passwordRaw)
        {
            var response = HttpContext.Current.Request.RequestContext.HttpContext.Response;
            var user = provider.GetByLogin(login);
            if (user == null)
                return false;

            if (Resolver.GetInstance<ICryptoProcessor<MD5>>().CryptPassword(user, passwordRaw) != user.PasswordHash)
                return false;

            provider.CreateOrUpdate(user);
            response.Cookies.Add(new HttpCookie("aayw-login", user.Id.ToString()));

            return true;
        }

        public bool Logout()
        {
            var response = HttpContext.Current.Request.RequestContext.HttpContext.Response;
            
            response.Cookies["aayw-login"].Expires = DateTime.Now.AddDays(-1d);

            return true;
        }

        public bool IsAvalibleForCreation(string login)
        {
            if (login.IsNullOrWhiteSpace())
                return true;

            var result = true;

            result = result && (provider.GetByLogin(login) == null);

            return result;
        }

        public bool IsAuthorized
        {
            get
            {
                var request = HttpContext.Current.Request.RequestContext.HttpContext.Request;
                return request.Cookies.AllKeys.Contains("aayw-login");
            }
            private set { }
        }

        public User CurrentUser
        {
            get
            {
                var request = HttpContext.Current.Request.RequestContext.HttpContext.Request;
                if (!IsAuthorized)
                    return null;

                return GetById(request.Cookies["aayw-login"].Value);
            }
            private set { }
        }
    }
}
