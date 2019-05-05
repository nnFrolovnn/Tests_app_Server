﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tests_server_app.Models.DBModels
{
    public class Answer
    {
        [Key]
        public long AnswerId { get; set; }

        [DataType(DataType.MultilineText)]
        public string AnswerText { get; set; }

        public virtual ICollection<QuestionAnswer> Questions { get; set; }

        public Answer()
        {
            Questions = new List<QuestionAnswer>();
        }
    }
}