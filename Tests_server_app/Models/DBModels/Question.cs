using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tests_server_app.Models.ViewModels;

namespace Tests_server_app.Models.DBModels
{
    public class Question
    {
        [Key]
        public long QuestionId { get; set; }

        [DataType(DataType.MultilineText)]
        public string QuestionText { get; set; }

        public int Weightiness { get; set; }

        public virtual ICollection<TestQuestion> Tests { get; set; }
        public virtual ICollection<QuestionAnswer> Answers { get; set; }

        public Question()
        {
            Tests = new List<TestQuestion>();
            Answers = new List<QuestionAnswer>();
        }

        public Question(TestQuestion testQuestion, QuestionVM q):this()
        {
            foreach(var a in q.Answers)
            {
                Answers.Add(new QuestionAnswer(this, a));
            }

            Weightiness = 1;
            QuestionText = q.QuestionText;

            Tests.Add(testQuestion);
        }
    }
}