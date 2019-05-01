using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Tests_server_app.Models.DBModels
{
    public class UserTest
    {
        public long UserId { get; set; }
        public User User { get; set; }

        public long TestId { get; set; }
        public Test Test { get; set; }

        public int CountAnsweredQuestions { get; set; }
        public int CountRightAnswers { get; set; }

        [DataType(DataType.Date)]
        public DateTime DatePassed { get; set; }
    }
}
