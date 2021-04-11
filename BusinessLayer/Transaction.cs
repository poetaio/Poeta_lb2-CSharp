using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;

namespace Wallets.BusinessLayer
{
    public class Transaction : EntityBase, IComparable<Transaction>
    {
        private decimal _sum;
        private string _currency;
        private Category _category;
        private string _description;
        private DateTime _date;
        private List<String> _files;
        private User _author;

        public decimal Sum { get => _sum; set => _sum = value; }
        public string Currency { get => _currency; set => _currency = value; }
        public Category Category { get => _category; set => _category = value; }
        public string Description { get => _description; set => _description = value; }
        public DateTime Date { get => _date; set => _date = value; }
        public List<string> Files { get => _files; set => _files = value; }
        public User Author { get => _author; set => _author = value; }

        public Transaction(DateTime date, decimal sum = 0, string description = "Default description", string currency = "USD", Category category = null,
            List<String> files = null)
        {
            Date = date;
            Sum = sum;
            Description = description;
            Currency = currency;
            Category = category == null ? new Category(Color.Black) : category;
            Files = files == null ? new List<string>() : files;
        }

        public int CompareTo(Transaction other)
        {
            if (other == null)
                return 1;

            int res = Date.CompareTo(other.Date);
            if (res == 0)
            {
                res = Sum.CompareTo(other.Sum);
                if (res == 0)
                {
                    res = Currency.CompareTo(other.Currency);

                    if (res == 0)
                        return Category.CompareTo(other.Category);
                    return res;

                }

                return res;
            }

            return res;
        }

        public override bool Validate()
        {
            return !String.IsNullOrWhiteSpace(Description) && new Regex("[A-Z]{3}").IsMatch(Currency) && Category != null && Date != null && Files != null;
        }

        public override string ToString()
        {
            string sum = (Sum > 0 ? "+" : "-") + Math.Abs(Sum);
            return $"Transaction \"{Description}\"" +
                $" {sum}{Currency} on {Date}" +
                $" Category: {Category}";
        }
    }
}
