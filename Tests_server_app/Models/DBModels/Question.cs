using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
    }
}