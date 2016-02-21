using AAYW.Core.Dependecies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Extras
{
    public class Singleton<TType>
    {
        protected Singleton()
        {

        }

        protected static TType _instance;

        public static TType Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resolver.GetInstance<TType>();
                }
                return _instance;
            }
        }
    }
}
