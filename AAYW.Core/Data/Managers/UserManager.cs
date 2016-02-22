using AAYW.Core.Data.Providers;
using AAYW.Core.Dependecies;
using AAYW.Models;
using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace AAYW.Core.Data.Managers
{
    public class UserManager
    {
        private UserProvider userProvider = Resolver.GetInstance<UserProvider>();

        public UserManager()
        {

        }

        public User GetById(string id)
        {
            return userProvider.GetById(id);
        }

        public IList<User> GetList()
        {
            return userProvider.GetList();
        }

        public bool Register(string login, string passwordRaw)
        {
            if (userProvider.GetByLogin(login) != null)
                return false;

            var newuser = Resolver.GetInstance<User>();

            newuser.PasswordHash = Encryptor.Instance.CryptPassword(newuser, passwordRaw);
            newuser.Login = login;

            userProvider.Create(newuser);

            return true;
        }

        public bool Login(string login, string passwordRaw)
        {
            var response = HttpContext.Current.Request.RequestContext.HttpContext.Response;
            var user = userProvider.GetByLogin(login);
            if (user == null)
                return false;

            if (Encryptor.Instance.CryptPassword(user, passwordRaw) != user.PasswordHash)
                return false;

            userProvider.Update(user);
            response.Cookies.Add(new HttpCookie("aayw-login", user.Id.ToString()));

            return true;
        }

        public bool Logout()
        {
            var response = HttpContext.Current.Request.RequestContext.HttpContext.Response;
            
            response.Cookies["aayw-login"].Expires = DateTime.Now.AddDays(-1d);

            return true;
        }

        public bool Update(User user)
        {
            userProvider.Update(user);
            return true;
        }

        public bool IsAvalibleForCreation(string login)
        {
            if (String.IsNullOrWhiteSpace(login))
                return true;

            var result = true;

            result = result && (userProvider.GetByLogin(login) == null);

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
