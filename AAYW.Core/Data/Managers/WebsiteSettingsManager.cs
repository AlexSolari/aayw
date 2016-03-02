using AAYW.Core.Data.Providers;
using AAYW.Core.Dependecies;
using AAYW.Core.Models.Bussines;
using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using AAYW.Core.Extensions;
using AAYW.Core.Crypto;
using AAYW.Core.Models.Bussines.User;
using System.Security.Cryptography;
using AAYW.Core.Annotations;

namespace AAYW.Core.Data.Managers
{
    public class WebsiteSettingsManager : BaseManager<WebsiteSettingsProvider, WebsiteSettings>
    {
        public WebsiteSettingsManager()
        {

        }

        public WebsiteSettings GetSettings()
        {
            var model = provider.GetList().FirstOrDefault();

            if (model == null)
            {
                model = Resolver.GetInstance<WebsiteSettings>();
                Create(model);
            }

            return model;
        }

        public void UpdateSettings(WebsiteSettings model)
        {
            var modelToUpdate = provider.GetList().FirstOrDefault();

            if (model == null)
            {
                Create(model);
            }
            else
            {
                model.Id = modelToUpdate.Id;
                Update(model);
            }
        }

        private void Create(WebsiteSettings model)
        {
            provider.Create(model);
        }
    }
}
