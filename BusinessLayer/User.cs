using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace Wallets.BusinessLayer
{
    public class User : EntityBase, IComparable<User>
    {
        private string _name;
        private string _surname;
        private string _email;

        private List<Category> _categories;
        private List<Wallet> _wallets;

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

        public User(string name = "Default name", string surname = "Default description", string email = "email@gmail.com")
        {
            Name = name;
            Surname = surname;
            Email = email;
            Categories = new List<Category>();
            Wallets = new List<Wallet>();
        }

        public void ShareWallet(String walletName, User withUser)
        {
            Wallet walletToShare = Wallets.FirstOrDefault(wallet => wallet.Name == walletName);

            if (walletToShare == null)
                throw new ArgumentException("No such wallet");

            withUser.Wallets.Add(walletToShare);
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
