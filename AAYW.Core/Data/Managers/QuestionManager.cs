using AAYW.Core.Data.Providers;
using AAYW.DependecyResolver;
using AAYW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AAYW.Core.Data.Managers
{
    public class QuestionManager
    {
        private QuestionProvider QuestionProvider = Resolver.GetInstance<QuestionProvider>();

        public QuestionManager()
        {

        }

        public Question GetById(string id)
        {
            return QuestionProvider.GetById(id);
        }

        public void IncrementViews(string id)
        {
            var question = GetById(id);
            question.Views++;
            QuestionProvider.Update(question);
        }

        public IList<Question> GetList()
        {
            return QuestionProvider.GetList();
        }

        public Question Create(string description, string title, User author = null)
        {
            var newQuestion = Resolver.GetInstance<Question>();

            if (string.IsNullOrWhiteSpace(description) || string.IsNullOrWhiteSpace(title))
            {
                return null;
            }

            newQuestion.AutorId = author?.Id ?? Guid.Empty;
            newQuestion.Views = 0;
            newQuestion.Description = description;
            newQuestion.Title = title;

            QuestionProvider.Create(newQuestion);

            return newQuestion;
        }
    }
}
