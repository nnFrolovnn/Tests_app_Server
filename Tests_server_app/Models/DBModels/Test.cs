using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Tests_server_app.Models.ViewModels;

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

        public bool Checked { get; set; }

        public virtual ICollection<TestQuestion> Questions { get; set; }
        public virtual ICollection<TestTheme> Themes { get; set; }
        public virtual ICollection<UserTest> Users { get; set; }

        public Test()
        {
            Questions = new List<TestQuestion>();
            Themes = new List<TestTheme>();
            Users = new List<UserTest>();
        }
        
        /// <summary>
        /// create test without themes
        /// </summary>
        /// <param name="testVM"> model of test </param>
        public Test(TestVM testVM):this()
        {
            CreationDate = DateTime.Now.Date;

            foreach(var q in testVM.Questions)
            {
                Questions.Add(new TestQuestion(this, q));
            }
        }
    }
}
