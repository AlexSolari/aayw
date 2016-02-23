using AAYW.Models;
using System;
using System.Security.Cryptography;
using System.Text;

namespace AAYW.Core
{
    public class Encryptor
    {
        private Encryptor() { }

        private static Encryptor _Encryptor;

        public static Encryptor Instance
        {
            get
            {
                if (_Encryptor == null)
                {
                    _Encryptor = new Encryptor();
                }
                return _Encryptor;
            }
        }

        #region Methods
        private string GetSalt(Entity entity)
        {
            return MD5Hash(entity.Id);
        }

        public string MD5Hash(Guid guid)
        {
            return MD5Hash(guid.ToString());
        }

        public string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            var result = md5.Hash;

            var strBuilder = new StringBuilder();
            foreach (var item in result)
            {
                strBuilder.Append(item.ToString("x2"));
            }

            return strBuilder.ToString();
        }

        public string CryptPassword(Entity entity, string password)
        {
            return MD5Hash(password + GetSalt(entity));
        }
        #endregion

    }
}
