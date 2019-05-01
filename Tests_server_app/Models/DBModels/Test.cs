using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Tests_server_app.Models.DBModels
{
    public class Test
    {
        [Key]
        public long TestId { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }

        public ulong LikesCount { get; set; }

        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public bool Cheched { get; set; }

        public virtual List<TestQuestion> Questions { get; set; }
        public virtual List<TestTheme> Themes { get; set; }
        public virtual List<UserTest> Users { get; set; }
    }
}
