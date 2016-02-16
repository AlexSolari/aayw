using AAYW.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Database.Mappings
{
    class AnswerMap : EntityMap<Answer>
    {
        public AnswerMap() : base()
        {
            Map(x => x.Description).Length(1000);
            Map(x => x.QuestionId);

            Table("Answers");
        }
    }
}
