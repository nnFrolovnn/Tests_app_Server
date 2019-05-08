using Tests_server_app.Models.ViewModels;

namespace Tests_server_app.Models.DBModels
{
    public class QuestionAnswer
    {
        public QuestionAnswer(Question question, AnswerVM a)
        {
            Question = question;
            Answer = new Answer(this, a);

            IsRight = a.IsRight;
        }

        public QuestionAnswer()
        {

        }

        public long QuestionId { get; set; }
        public virtual Question Question { get; set; }

        public long AnswerId { get; set; }
        public virtual Answer Answer { get; set; }

        public bool IsRight { get; set; }
    }
}