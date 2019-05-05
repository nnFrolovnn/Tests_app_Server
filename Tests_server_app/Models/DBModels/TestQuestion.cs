namespace Tests_server_app.Models.DBModels
{
    public class TestQuestion
    {
        public long TestId { get; set; }
        public virtual Test Test { get; set; }

        public long QuestionId { get; set; }
        public virtual Question Question { get; set; }

        public double WeightValue { get; set; }
    }
}