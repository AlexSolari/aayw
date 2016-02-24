using AAYW.Core.Models.Bussines;
using System;
using System.Security.Cryptography;
using System.Text;

namespace AAYW.Core.Crypto
{
    public class BaseCryptoProcessor : ICryptoProcessor<MD5>
    {
        public MD5 Hasher { get; set; }

        public BaseCryptoProcessor()
        {
            Hasher = new MD5CryptoServiceProvider();
        }

        public string SaltFor(Entity entity)
        {
            return Hash(entity.Id);
        }

        public string Hash(Guid guid)
        {
            return Hash(guid.ToString());
        }

        public string Hash(string text)
        {
            Hasher.ComputeHash(Encoding.ASCII.GetBytes(text));
            var result = Hasher.Hash;

            var strBuilder = new StringBuilder();
            foreach (var item in result)
            {
                strBuilder.Append(item.ToString("x2"));
            }

            return strBuilder.ToString();
        }

        public string CryptPassword(Entity entity, string password)
        {
            return Hash(Hash(password) + SaltFor(entity));
        }
    }
}
