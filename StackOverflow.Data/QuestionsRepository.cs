using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Data
{
    public class QuestionsRepository
    {
        private string _connectionString;

        public QuestionsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Question> GetAll()
        {
            var ctx = new QAContext(_connectionString);
            return ctx.Questions.Include(q => q.Answers).Include(q => q.QuestionsTags).ThenInclude(qt => qt.Tag).OrderByDescending(q => q.Id).ToList();
        }

        public Tag GetTag(string name)
        {
            using var ctx = new QAContext(_connectionString);
            return ctx.Tags.FirstOrDefault(t => t.Name == name);
        }

        public int AddTag(string name)
        {
            using var ctx = new QAContext(_connectionString);
            var tag = new Tag { Name = name };
            ctx.Tags.Add(tag);
            ctx.SaveChanges();
            return tag.Id;
        }

        public List<Question> GetQuestionsForTag(string name)
        {
            using var ctx = new QAContext(_connectionString);
            return ctx.Questions
                    .Where(c => c.QuestionsTags.Any(t => t.Tag.Name == name))
                    .Include(q => q.QuestionsTags)
                    .ThenInclude(qt => qt.Tag)
                    .ToList();
        }

        public void AddQuestion(Question question, List<string> tags)
        {
            using var ctx = new QAContext(_connectionString);
            ctx.Questions.Add(question);
            ctx.SaveChanges();
            foreach (string tag in tags)
            {
                Tag t = GetTag(tag);
                int tagId;
                if (t == null)
                {
                    tagId = AddTag(tag);
                }
                else
                {
                    tagId = t.Id;
                }
                ctx.QuestionsTags.Add(new QuestionsTags
                {
                    QuestionId = question.Id,
                    TagId = tagId
                });
            }

            ctx.SaveChanges();
        }

        public Question GetQuestionWithAnswerById(int id)
        {
            using var ctx = new QAContext(_connectionString);
            return ctx.Questions.Where(q => q.Id == id).Include(q => q.Person).Include(q => q.Answers)
            .ThenInclude(a => a.Person).Include(q => q.QuestionsTags).ThenInclude(qt => qt.Tag).FirstOrDefault();
        }

        public void AddAnswer(Answer answer)
        {
            var ctx = new QAContext(_connectionString);

            ctx.Answers.Add(answer);
            ctx.SaveChanges();
        }
    }
}
