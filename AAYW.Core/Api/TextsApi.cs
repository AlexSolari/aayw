using AAYW.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Api
{
    public static partial class SiteApi
    {
        public class TextsApi
        {
            private TextsApi()
            {

            }

            private static TextsApi _instance;

            public static TextsApi Instance
            {
                get
                {
                    if (_instance == null)
                        _instance = new TextsApi();
                    return _instance;
                }
            }

            public string Get(string key)
            {
                var value = ResourceAccessor.Instance.Get(key);
                return (string.IsNullOrWhiteSpace(value)) ? string.Format("[{0}]", key) : value;
            }
        }
    }
    
}
