using AAYW.Core.Data.Providers;
using AAYW.Core.Dependecies;
using AAYW.Core.Models.Bussines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Data.Managers
{
    public class BaseManager<TProvider, TModel> : IManager<TModel>
        where TProvider : IProvider<TModel>
        where TModel : Entity
    {
        protected TProvider provider = Resolver.GetInstance<TProvider>();

        public BaseManager()
        {

        }

        public TModel GetById(string id)
        {
            return provider.GetById(id);
        }

        public IList<TModel> GetList(int page = 0, int pagesize = 50)
        {
            return provider.GetList(page, pagesize);
        }

        public bool Update(TModel model)
        {
            provider.Update(model);
            return true;
        }

        public TModel GetByField(string field, string value)
        {
            return provider.GetByField(field, value);
        }

        public IList<TModel> GeListByField(string field, string value)
        {
            return provider.GetListByField(field, value);
        }
    }
}
