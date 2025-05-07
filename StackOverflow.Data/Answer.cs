using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Data
{
    public class Answer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int PersonId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public Person Person { get; set; }
    }
}
