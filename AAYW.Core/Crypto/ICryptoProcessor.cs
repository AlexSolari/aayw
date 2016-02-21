using AAYW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Crypto
{
    public interface ICryptoProcessor
    {
        string MD5Hash(Guid guid);
        string MD5Hash(string text);
        string CryptPassword(Entity entity, string password);
        string GetSalt(Entity entity);
    }
}
