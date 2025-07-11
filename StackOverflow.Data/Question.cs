﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Data
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime DatePosted { get; set; }
        public int PersonId { get; set; }

        public Person Person { get; set; }

        public List<QuestionsTags> QuestionsTags { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
