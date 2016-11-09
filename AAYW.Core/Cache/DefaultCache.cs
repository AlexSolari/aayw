using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Cache
{
    public class DefaultCache : ICache
    {
        public bool OverwriteMode = true;

        private Dictionary<string, object> Values = new Dictionary<string, object>();

        public T Get<T>(string key)
        {
            return (T)Values[key];
        }

        public void Drop(string key)
        {
            Values.Remove(key);
        }

        public void DropAll()
        {
            Values.Clear();
        }

        public void Add<T>(T value, string key)
        {
            if (OverwriteMode)
            {
                Drop(key);
            }

            Values.Add(key, value);
        }

        public bool HasKey(string key)
        {
            return Values.ContainsKey(key);
        }

        public void DropWhere(Predicate<string> comparer)
        {
            Values = Values.Where(x => !comparer(x.Key)).ToDictionary(x => x.Key, x => x.Value);
        }

        public Dictionary<string, object> GetAll()
        {
            return Values;
        }
    }
}
