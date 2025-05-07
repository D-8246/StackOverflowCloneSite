using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.Data;
using StackOverflow.Web.Models;
using System.Diagnostics;

namespace StackOverflow.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString;
        public HomeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        public IActionResult Index()
        {
            var qrepo = new QuestionsRepository(_connectionString);
            var questions = qrepo.GetAll();
            return View(questions);
        }

        [Authorize]
        public IActionResult AskAQuestion()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(Question question, List<string> tags)
        {
            var qrepo = new QuestionsRepository(_connectionString);
            var urepo = new UserRepository(_connectionString);
            var person = urepo.GetByEmail(User.Identity.Name);
            question.PersonId = person.Id;
            qrepo.AddQuestion(question, tags);
            return Redirect("/");
        }

        public IActionResult ViewQuestion(int id)
        {
            var qrepo = new QuestionsRepository(_connectionString);
            var question = qrepo.GetQuestionWithAnswerById(id);
            return View(question);
        }

        [HttpPost]
        public IActionResult AddAnswer(Answer answer)
        {
            var qrepo = new QuestionsRepository(_connectionString);
            var urepo = new UserRepository(_connectionString);
            var person = urepo.GetByEmail(User.Identity.Name);
            answer.PersonId = person.Id;
            answer.Date = DateTime.Now;
            qrepo.AddAnswer(answer);
            return Redirect($"/home/viewquestion?id={answer.QuestionId}");
        }
    }
}
