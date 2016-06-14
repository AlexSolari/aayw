using AAYW.Core.Data.Providers;
using AAYW.Core.Dependecies;
using AAYW.Core.Models.Bussines;
using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using AAYW.Core.Crypto;
using AAYW.Core.Models.Bussines.User;
using System.Security.Cryptography;
using AAYW.Core.Annotations;
using AAYW.Core.Models.Admin.Bussines;

namespace AAYW.Core.Data.Managers
{
    [ManagerFor(typeof(WebsiteSetting))]
    public class WebsiteSettingsManager : BaseManager<WebsiteSettingsProvider, WebsiteSetting>
    {
        public WebsiteSettingsManager() : base() { }
        public WebsiteSettingsManager(bool suppressLogging) : base(suppressLogging) { }

        public WebsiteSetting GetSettings()
        {
            var model = provider.GetList().FirstOrDefault();

            if (model == null)
            {
                model = Resolver.GetInstance<WebsiteSetting>();
                CreateOrUpdate(model);
            }

            return model;
        }

        public void UpdateSettings(WebsiteSetting model)
        {
            var modelToUpdate = provider.GetList().FirstOrDefault();

            if (model != null)
            {
                model.Id = modelToUpdate.Id;
            }

            CreateOrUpdate(model);
        }
    }
}
