using AAYW.Core.Data.Providers;
using AAYW.Core.Dependecies;
using AAYW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Data.Managers
{
    public class AnswerManager
    {
        private AnswerProvider answerProvider = Resolver.GetInstance<AnswerProvider>();

        public IList<Answer> GetList(string questionId)
        {
            return answerProvider.GetList(Guid.Parse(questionId));
        }

        public void Create(string text, Guid parentId)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return;
            }

            var answer = Resolver.GetInstance<Answer>();

            answer.Description = text;
            answer.QuestionId = parentId;

            answerProvider.Create(answer);
        }
    }
}
