using AAYW.Core.Dependecies;
using AAYW.Core.Models.Admin.Bussines;
using AAYW.Core.Models.Bussines.Theme;
using AAYW.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AAYW.Core.Api
{
    public static partial class SiteApi
    {
        public class FrontendApi
        {
            private FrontendApi()
            {

            }

            private static FrontendApi _instance;

            public static FrontendApi Instance
            {
                get
                {
                    if (_instance == null)
                        _instance = new FrontendApi();
                    return _instance;
                }
            }

            public Theme CurrentTheme
            {
                get
                {
                    var settings = SiteApi.Data.WebsiteSettings.All().FirstOrDefault();
                    
                    if (settings == null)
                        return DefaultTheme;

                    var theme = settings.CurrentTheme.DeserializeAs<Theme>();
                    
                    if (theme == null)
                        return DefaultTheme;

                    return theme;
                }

                set
                {
                    var settings = SiteApi.Data.WebsiteSettings.All().FirstOrDefault() ?? Resolver.GetInstance<WebsiteSetting>();

                    var serialized = value.Serialize();

                    settings.CurrentTheme = new XmlDocument();
                    settings.CurrentTheme.LoadXml(serialized);

                    SiteApi.Data.WebsiteSettings.CreateOrUpdate(settings);
                }
            }

            public Theme DefaultTheme
            {
                get
                {
                    return new Theme()
                    {
                        Primary = "#2196F3",
                        PrimaryText = "#FFFFFF",
                        Danger = "#f55a4e",
                        DangerAccent = "#FF4081",
                        DangerPrimary = "#F44336",
                        Divider = "#E0E0E0",
                        SecondaryText = "#a6a6a6",
                        DarkPrimary = "#1976D2",
                        DarkPrimaryText = "#2D2D2D",
                        Accent = "#39a1f4"
                    };
                }
            }
        }
    }
    
}
