using System.Collections.Generic;
using Tests_server_app.Models.DBModels;

namespace Tests_server_app.Models.ViewModels
{
    public class QuestionVM
    {
        public string QuestionText { get; set; }
        public List<AnswerVM> Answers { get; set; }


        public QuestionVM(TestQuestion q)
        {
            QuestionText = q.Question?.QuestionText;

            Answers = new List<AnswerVM>();
            foreach(var a in q.Question?.Answers)
            {
                Answers.Add(new AnswerVM(a));
            }
        }
    }
}