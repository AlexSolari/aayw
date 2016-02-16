using AAYW.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Database.Mappings
{
    class QuestionMap : EntityMap<Question>
    {
        public QuestionMap() : base()
        {
            Map(x => x.Views);
            Map(x => x.Description).Length(1000);
            Map(x => x.Title).Length(400);
            Map(x => x.AutorId);

            Table("Questions");
        }
    }
}
