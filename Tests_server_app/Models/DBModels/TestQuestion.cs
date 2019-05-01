namespace Tests_server_app.Models.DBModels
{
    public class TestQuestion
    {
        public long TestId { get; set; }
        public Test Test { get; set; }

        public long QuestionId { get; set; }
        public Question Question { get; set; }

        public double WeightValue { get; set; }
    }
}