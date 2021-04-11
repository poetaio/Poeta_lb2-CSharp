﻿using System;

namespace Wallets.Models
{
    public class User
    {
        public Guid Guid { get; }

        public User(Guid guid, string firstName, string lastName, string email, string login)
        {
            Guid = guid;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Login = login;
        }

        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public string Login { get; }
    }
}
