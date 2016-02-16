using AAYW.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Database.Mappings
{
    class EntityMap<TEntity> : ClassMap<TEntity>
        where TEntity : Entity
    {
        public EntityMap()
        {
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.CreationTime);
        }
    }
}
