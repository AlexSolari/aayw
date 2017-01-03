using AAYW.Core.Annotations;
using AAYW.Core.Data.Providers;
using AAYW.Core.Models.Bussines.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Data.Managers
{
    [ManagerFor(typeof(ContentBlock))]
    public class ContentBlockManager : BaseManager<ContentBlockProvider, ContentBlock>, IManager<ContentBlock>
    {
        public ContentBlockManager() : base() { }
        public ContentBlockManager(bool suppressLogging) : base(suppressLogging) { }

        public override void CreateOrUpdate(ContentBlock model)
        {
            base.CreateOrUpdate(model);
        }

        private bool UpdateNeeded(ContentBlock model)
        {
            var secondModel = GetById(model.Id.ToString());

            return (secondModel != null);
        }
    }
}
