using AAYW.Core.Cache;
using AAYW.Core.Crypto;
using AAYW.Core.Dependecies;
using AAYW.Core.Logging;
using AAYW.Core.Mail;
using AAYW.Core.Reflector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AAYW.Core.Api
{
    public static partial class SiteApi
    {
        public class SiteServiceApi
        {
            private SiteServiceApi()
            {

            }

            private static SiteServiceApi _instance;

            public static SiteServiceApi Instance
            {
                get
                {
                    if (_instance == null)
                        _instance = new SiteServiceApi();
                    return _instance;
                }
            }

            public IMailProcessor Mailer
            {
                get { return Resolver.GetInstance<IMailProcessor>(); }
            }

            public IReflector Reflector
            {
                get { return Resolver.GetInstance<IReflector>(); }
            }

            public ILogger Logger
            {
                get { return Resolver.GetInstance<ILogger>(); }
            }

            public ICache Cache
            {
                get { return Resolver.GetInstance<ICache>(); }
            }
        }
    }
}
