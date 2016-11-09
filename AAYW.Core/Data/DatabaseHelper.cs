using AAYW.Core.Api;
//#define RECREATE_DATABASE
using sORM.Core;

namespace AAYW.Core.Data
{
    public class DatabaseHelper
    {
        public static void Initialize()
        {
            SimpleORM.Current.Initialize(System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
            SimpleORM.Current.AddOnRequestListener((sql) => SiteApi.Services.Logger.Log(" >>> " + sql));
        }
    }
}