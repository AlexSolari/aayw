﻿using AAYW.Core.Data.Providers;
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

        public virtual TModel GetById(string id)
        {
            return provider.GetById(id);
        }

        public virtual IList<TModel> GetList(int page = 0, int pagesize = 50)
        {
            return provider.GetList(page, pagesize);
        }

        public virtual void CreateOrUpdate(TModel model)
        {
            provider.CreateOrUpdate(model);
        }

        public virtual void Delete(TModel model)
        {
            provider.Delete(model);
        }

        public virtual TModel GetByField(string field, string value)
        {
            return provider.GetByField(field, value);
        }

        public virtual IList<TModel> GetListByField(string field, string value)
        {
            return provider.GetListByField(field, value);
        }
    }
}
