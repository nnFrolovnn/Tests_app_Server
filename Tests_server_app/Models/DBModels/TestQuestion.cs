using Tests_server_app.Models.ViewModels;

namespace Tests_server_app.Models.DBModels
{
    public class TestQuestion
    {
        public TestQuestion(Test test, QuestionVM q)
        {
            Test = test;

            Question = new Question(this, q);
            WeightValue = 1;
        }

        public TestQuestion()
        {

        }

        public long TestId { get; set; }
        public virtual Test Test { get; set; }

        public long QuestionId { get; set; }
        public virtual Question Question { get; set; }

        public double WeightValue { get; set; }
    }
}