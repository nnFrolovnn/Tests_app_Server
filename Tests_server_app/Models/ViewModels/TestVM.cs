using System;
using System.Collections.Generic;
using Tests_server_app.Models.DBModels;

namespace Tests_server_app.Models.ViewModels
{
    public class TestVM
    {

        public TestVM(UserTest test)
        {
            LikesCount = test.Test?.LikesCount;
            Title = test.Test?.Title;
            Description = test.Test?.Description;
            PassedDate = test?.DatePassed;
            RightQnswers = test?.CountRightAnswers;

            Questions = new List<QuestionVM>();
            foreach (var q in test.Test?.Questions)
            {
                Questions.Add(new QuestionVM(q));
            }

            Themes = new List<ThemeVM>();
            foreach(var t in test.Test?.Themes)
            {
                Themes.Add(new ThemeVM(t));
            }    
        }

        public TestVM(Test test)
        {
            LikesCount = test.LikesCount;
            Title = test.Title;
            Description = test.Description;
            PassedDate = DateTime.MinValue;
            RightQnswers = -1;

            Questions = new List<QuestionVM>();
            foreach (var q in test.Questions)
            {
                Questions.Add(new QuestionVM(q));
            }

            Themes = new List<ThemeVM>();
            foreach (var t in test.Themes)
            {
                Themes.Add(new ThemeVM(t));
            }
        }

        public ulong? LikesCount { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? PassedDate { get; set; }
        public int? RightQnswers { get; set; }

        public List<QuestionVM> Questions { get; set; }
        public List<ThemeVM> Themes { get; set; }
    }
}