using StackOverflow.Data;

namespace StackOverflow.Web.Models
{
    public class ViewQuestionViewModel
    {
        public Question Question { get; set; }
        public Person Person { get; set; }
        public List<AnswerPersonObj> AnsPerObj { get; set; } = new();
        public List<Tag> Tags { get; set; } = new();
    }
}
