using System;
using System.Collections.Generic;
using Wallets.DataStorage;

namespace Wallets.BusinessLayer.Users 
{
    public class DBUser : IStorable
    {
        // private List<Guid> _walletsGuids;
        public Guid Guid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public List<Guid> WalletsGuids { get; set; }
        public List<Guid> SharedWalletsGuids { get; set; }
        public List<Guid> CategoriesGuids { get; set; }

        public DBUser(Guid guid, string firstName, string lastName, string email, string login, string password)
        {
            Guid = guid;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Login = login;
            Password = password;
            WalletsGuids = new List<Guid>();
            SharedWalletsGuids = new List<Guid>();
            CategoriesGuids = new List<Guid>();
        }
        // public List<Guid> WalletsGuids { get; }
    }
}
