using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;
using System.Linq;
using Wallets.BusinessLayer.Users;
using Wallets.DataStorage;

namespace Wallets.BusinessLayer
{
    public class Wallet : EntityBase, IStorable
    {
        private Guid _guid;
        private string _name;
        private string _description;
        private string _currency;
        private decimal _balance;
        private List<Category> _categoriesList;
        private List<Transaction> _transactionsList;

        public string Name { get => _name; set => _name = value; }
        public decimal Balance
        {
            get { return _balance; }
            private set { }
        }
        public string Description { get => _description; set => _description = value; }
        public string Currency { get => _currency; set => _currency = value; }

        public Guid Guid { get => _guid; }

        public Wallet(string name = "Default name", string description = "Default description", string currency = "USD", decimal balance = 0)
        {
            _guid = Guid.NewGuid();
            Name = name;
            Description = description;
            Currency = currency;
            Balance = balance;
            _categoriesList = new List<Category>();
            _transactionsList = new List<Transaction>();
        }

        public void AddTransaction(Transaction newTransaction)
        {
            _transactionsList.Add(newTransaction);
            _balance += newTransaction.Sum;
        }

        public void RemoveTransaction(int index)
        {
            if (index < 0 || index > _transactionsList.Count)
                throw new IndexOutOfRangeException("Wrong index");

            Balance -= _transactionsList[index].Sum;
            _transactionsList.RemoveAt(index);
        }

        // setters
        public void SetTransactionSum(int index, decimal newSum)
        {
            Balance += -_transactionsList[index].Sum + newSum;
            _transactionsList[index].Sum = newSum;
        }

        public void SetTransactionDescription(int index, string newDescription)
        {
            _transactionsList[index].Description = newDescription;
        }

        public void SetTransactionCurrency(int index, string newCurrency)
        {
            _transactionsList[index].Currency = newCurrency;
        }

        public void SetTransactionCategory(int index, Category newCategory)
        {
            // manage available categories
            _transactionsList[index].Category = newCategory;
        }

        public void SetTransactionDate(int index, DateTime newDate)
        {
            _transactionsList[index].Date = newDate;
        }

        // getters
        // use ref keyword for readonly transaction by index
        // redefine [] operator
        public Transaction this[int index]
        {
            get => _transactionsList[index].Copy();
            private set => _transactionsList[index] = value;
        }

        public List<Transaction> GetAllTransactions()
        {
            return _transactionsList.Select(tran => tran.Copy()).ToList();
        }
        public List<Category> GetAllCategories()
        {
            return _categoriesList.Select(cat => cat.Copy()).ToList();
        }

        public void AddCategory(Category newCategory)
        {
            _categoriesList.Add(newCategory);
        }

        public decimal GetLastMonthIncome()
        {
            return LastMonthTransactionsTotal(true);
        }

        public decimal GetLastMonthExpenses()
        {
            return LastMonthTransactionsTotal(false);
        }

        public void SortBySum()
        {
            _transactionsList.Sort((a, b) => a.Sum.CompareTo(b.Sum));
        }

        public void SortByCurrency()
        {
            _transactionsList.Sort((a, b) => a.Currency.CompareTo(b.Currency));
        }

        public void SortByCategory()
        {
            _transactionsList.Sort((a, b) => a.Category.CompareTo(b.Category));
        }

        public void SortByDate()
        {
            _transactionsList.Sort((a, b) => a.Date.CompareTo(b.Date));
        }

        public void SortByUser()
        {
            _transactionsList.Sort((a, b) => a.Author.CompareTo(b.Author));
        }

        public void Reverse()
        {
            _transactionsList.Reverse();
        }

        
        public List<Transaction> GetTransactionsByUser(User user)
        {
            return _transactionsList.FindAll(transaction => transaction.Author == user)
                .Select(tran => tran.Copy())
                .ToList();
        }

        public List<Transaction> GetTransactionsByOtherUsers(User user)
        {
            return _transactionsList.FindAll(transaction => transaction.Author != user)
                .Select(tran => tran.Copy())
                .ToList();
        }

        public override bool Validate()
        {
            return !String.IsNullOrEmpty(Name) && !String.IsNullOrEmpty(Description) &&
                new Regex("[A-Z]{3}").IsMatch(Currency);
        }

        public List<Transaction> DisplayTenTransactions(int from, int numberOfTransactions)
        {
            if (from < 0 || from >= _transactionsList.Count || numberOfTransactions < 1 || numberOfTransactions > 10)
                throw new ArgumentException("Invalid arguments");

            return _transactionsList.GetRange(from, numberOfTransactions)
                .Select(tran => tran.Copy())
                .ToList();
        }

        private decimal LastMonthTransactionsTotal(bool positive)
        {
            Transaction monthAgoTransaction = new Transaction(DateTime.Now.AddDays(-30));

            decimal result = 0.0m;
            foreach (Transaction transaction in _transactionsList)
            {
                if (transaction.CompareTo(monthAgoTransaction) >= 0)
                    if (positive && transaction.Sum > 0)
                        result += transaction.Sum;
                    else if (!positive && transaction.Sum < 0)
                        result += -transaction.Sum;
            }

            return result;
        }
    }
}
