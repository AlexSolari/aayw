using AAYW.Core.Models.Bussines;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Models.Mappings
{
    class EntityMap<TEntity> : ClassMap<TEntity>
        where TEntity : Entity
    {
        public EntityMap()
        {
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.CreatedDate);
            Map(x => x.ModifiedDate);
        }

        public void CrateTable()
        {
            Table("{0}s".FormatWith(typeof(TEntity).Name));
        }
    }
}
