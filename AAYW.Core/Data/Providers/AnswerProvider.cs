using AAYW.Database;
using AAYW.Models;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Data.Providers
{
    public class AnswerProvider : DataProvider<Answer>
    {
        public IList<Answer> GetList(Guid questionId)
        {
            IList<Answer> result = new List<Answer>();
            Execute(session =>
            {
                var criteria = session.CreateCriteria(typeof(Answer));
                criteria.Add(Restrictions.Eq("QuestionId", questionId));
                criteria.AddOrder(Order.Desc("CreationTime"));
                result = criteria.List<Answer>();
            });
            return result;
        }
    }
}
