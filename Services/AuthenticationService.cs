using System;
using System.Collections.Generic;
using System.Linq;
using Wallets.Models;

namespace Wallets.Services
{
    public class AuthenticationService
    {
        private static List<DBUser> _users = new List<DBUser>();
        public User Authenticate(AuthenticationUser authUser)
        {
            if (String.IsNullOrWhiteSpace(authUser.Login) || String.IsNullOrWhiteSpace(authUser.Password))
                throw new ArgumentException("Login or Password is Empty");

            var dbUser = _users.FirstOrDefault(user => user.Login == authUser.Login && user.Password == authUser.Password);

            if (dbUser == null)
                throw new Exception("Wrong Login or Password");

            return new User(dbUser.Guid, dbUser.FirstName, dbUser.LastName, dbUser.Email, dbUser.Login);
        }

        public bool RegisterUser(RegistrationUser regUser)
        {
            var dbUser = _users.FirstOrDefault(user => user.Login == regUser.Login);
            if (dbUser != null)
                throw new Exception("user already exists");

            if (String.IsNullOrWhiteSpace(regUser.Login) || String.IsNullOrWhiteSpace(regUser.Password))
                throw new ArgumentException("Login or Password is Empty");

            dbUser = new DBUser(regUser.LastName, regUser.LastName, regUser.LastName, regUser.Login, regUser.Password);
            _users.Add(dbUser);
            return true;
        }
    }
}
