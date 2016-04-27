using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Cache
{
    public interface ICache
    {
        T Get<T>(string key);
        void Add<T>(T value, string key);
        void Drop(string key);
        void DropAll();
        void DropWhere(Predicate<string> comparer);
        bool HasKey(string key);
        Dictionary<string, object> GetAll();
    }
}
