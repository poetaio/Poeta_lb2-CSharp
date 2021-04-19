using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Wallets.DataStorage;
using System.Linq;

namespace Wallets.BusinessLayer.Users
{
    public class User : EntityBase, IComparable<User>, IStorable
    {
        private string _name;
        private string _surname;
        private string _email;
        private string _login;

        private List<Category> _categories;
        private List<Wallet> _wallets;
        private List<Wallet> _sharedWallets;
        
        public Guid Guid { get; set; }
        public string Name { get => _name; set => _name = value; }
        public string Surname { get => _surname; set => _surname = value; }
        public string FullName
        {
            get => $"{Name} {Surname}".Trim();
            private set { }
        }
        public string Email { get => _email; set => _email = value; }
        public List<Category> Categories { get => _categories; set => _categories = value; }
        public List<Wallet> Wallets { get => _wallets; set => _wallets = value; }
        public List<Wallet> SharedWallets { get => _sharedWallets; set => _sharedWallets = value; }
        public string Login { get => _login; set => _login = value; }
        
        public User(Guid guid, string name = "Default name", string surname = "Default surname", 
            string email = "email@gmail.com", string login = "email.@gmail.com")
        {
            Guid = guid;
            Name = name;
            Surname = surname;
            Email = email;
            Login = login;
            Categories = new List<Category>();
            Wallets = new List<Wallet>();
            SharedWallets = new List<Wallet>();
        }

        public void ShareWallet(Guid walletGuid, User withUser)
        {
            Wallet walletToShare = Wallets.FirstOrDefault(wallet => wallet.Guid == walletGuid);

            if (walletToShare == null)
                throw new ArgumentException("No such wallet");

            withUser.SharedWallets.Add(walletToShare);
        }

        public override bool Validate()
        {
            return !String.IsNullOrEmpty(Name) && !String.IsNullOrEmpty(Surname)
                && new Regex("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$").IsMatch(Email)
                && Categories != null && Wallets != null;
        }

        public int CompareTo(User other)
        {
            if (other == null)
                return 1;

            int result = FullName.CompareTo(other.FullName);

            if (result == 1)
                return Email.CompareTo(other.Email);

            return result;
        }
    }
}
