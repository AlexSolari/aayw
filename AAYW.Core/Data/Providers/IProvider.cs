using AAYW.Core.Models.Bussines;
using System;

namespace AAYW.Core.Data.Providers
{
    public interface IProvider<TEntity>
     where TEntity : Entity
    {
        void Delete(TEntity model);
        TEntity GetByField(string field, string value);
        TEntity GetById(Guid id);
        TEntity GetById(string id);
        System.Collections.Generic.IList<TEntity> GetList(int page, int pagesize);
        System.Collections.Generic.IList<TEntity> All();
        System.Collections.Generic.IList<TEntity> GetListByField(string field, string value);
        void CreateOrUpdate(TEntity model);
    }
}
