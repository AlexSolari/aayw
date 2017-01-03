using AAYW.Core.Models.Bussines;
using System;
using System.Collections.Generic;

namespace AAYW.Core.Data.Managers
{
    public interface IManager<TModel>
     where TModel : Entity
    {
        IList<TModel> GetListByField(string field, object value);
        TModel GetByField(string field, object value);
        TModel GetById(string id);
        IList<TModel> GetList(int page, int pagesize = 50);
        IList<TModel> All();
        void Delete(TModel model);
        void CreateOrUpdate(TModel model);
    }
}
