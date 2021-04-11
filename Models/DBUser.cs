using System;

namespace Wallets.Models
{
    public class DBUser
    {

        public DBUser(string firstName, string lastName, string email, string login, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Login = login;
            Password = password;
            Guid = new Guid();
        }

        public Guid Guid { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public string Login { get; }
        public string Password { get; }
    }
}
