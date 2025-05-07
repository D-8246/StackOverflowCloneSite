using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using StackOverflow.Data;
using System.Security.Claims;

namespace StackOverflow.Web.Controllers
{
    public class AccountController : Controller
    {
        private string _connectionString = @"Data Source=.\sqlexpress;Initial Catalog=QA;Integrated Security=true;Trust Server Certificate=true;";

        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Signup(Person person, string password)
        {
            var urepo = new UserRepository(_connectionString);
            urepo.Add(person, password);
            return Redirect("/Account/Login");
        }

        public IActionResult Login()
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var urepo = new UserRepository(_connectionString);
            var person = urepo.Login(email, password);
            if (person == null)
            {
                TempData["Message"] = "Invalid login, try again.";
                return Redirect("/account/login");
            }
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, email)
            };
            HttpContext.SignInAsync(new ClaimsPrincipal(
                    new ClaimsIdentity(claims, "Cookies", ClaimTypes.Email, "roles"))
                ).Wait();

            return Redirect("/home/index");
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync().Wait();
            return Redirect("/account/login");
        }
    }
}
