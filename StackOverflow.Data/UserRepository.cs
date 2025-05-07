using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Data
{
    public class UserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Person person, string password)
        {
            string hash = BCrypt.Net.BCrypt.HashPassword(password);
            person.PasswordHash = hash;
            QAContext ctx = new(_connectionString);
            ctx.People.Add(person);
            ctx.SaveChanges();
        }

        public Person Login(string email, string password)
        {
            var person = GetByEmail(email);
            if (person == null)
            {
                return null;
            }

            bool isMatch = BCrypt.Net.BCrypt.Verify(password, person.PasswordHash);
            return isMatch ? person : null;
        }

        public Person GetByEmail(string email)
        {
            QAContext ctx = new(_connectionString);
            return ctx.People.FirstOrDefault(p => p.Email == email);
        }
    }
}
