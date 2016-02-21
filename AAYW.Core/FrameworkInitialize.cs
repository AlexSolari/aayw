using AAYW.Core.Crypto;
using AAYW.Core.Dependecies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core
{
    public static class Framework
    {
        public static void Initialize()
        {
            // Register types here, and call this in Global.asax
            // Example:
            //  Resolver.RegisterType<TBase, TDerived>();
            //  Resolver.RegisterType<IInterface, TRealisation>();
            Resolver.RegisterType<ICryptoProcessor, BaseCryptoProcessor>();
        }
    }
}
