using AAYW.Core.Models.Bussines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Crypto
{
    public interface ICryptoProcessor<THasher>
        where THasher : HashAlgorithm
    {
        THasher Hasher { get; set; }
        string Hash(Guid guid);
        string Hash(string text);
        string CryptPassword(Entity entity, string password);
        string SaltFor(Entity entity);
    }
}
