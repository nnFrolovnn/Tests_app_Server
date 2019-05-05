using Tests_server_app.Models.DBModels;

namespace Tests_server_app.Models.ViewModels
{
    public class AnswerVM
    {
        public string AnswerText { get; set; }
        public bool IsRight { get; set; }

        public AnswerVM(QuestionAnswer a)
        {
            IsRight = a.IsRight;
            AnswerText = a.Answer?.AnswerText;
        }
    }
}