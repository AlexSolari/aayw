using AAYW.Core.Models.Bussines.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Data.Providers
{
    public class ContentBlockProvider : BaseProvider<ContentBlock>
    {
        public ContentBlockProvider() : base() { }
        public ContentBlockProvider(bool suppressLogging) : base(suppressLogging) { }
    }
}
