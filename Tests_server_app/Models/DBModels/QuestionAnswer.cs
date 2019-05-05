namespace Tests_server_app.Models.DBModels
{
    public class QuestionAnswer
    {
        public long QuestionId { get; set; }
        public virtual Question Question { get; set; }

        public long AnswerId { get; set; }
        public virtual Answer Answer { get; set; }

        public bool IsRight { get; set; }
    }
}