﻿using OopEksamen.Classes.Transactions;
using OopEksamen.Interfaces;
using OopEksamen.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopEksamen.Classes
{
    public class StregSystem : IStregSystem
    {
        private List<Product> _products { get; set; }
        private List<Transaction> _transactions { get; set; }
        private List<User> _users { get; set; }


        public IEnumerable<Product> ActiveProducts { get; set; } = new List<Product>();

        public event UserBalanceNotification UserBalanceWarning;

        private uint _nextTransactionId { get; set; } = 0;
        private uint getNewTransactionID() => _nextTransactionId++;

        public InsertCashTransaction AddCreditsToAccount(User user, Money amount)
        {
            return new InsertCashTransaction(getNewTransactionID(), user, amount);
        }

        public BuyTransaction BuyProduct(User user, Product product)
        {
            return new BuyTransaction(getNewTransactionID(), user, product);
        }

        public Product GetProductByID(uint id)
        {
            return _products.FirstOrDefault(i => i.ID == id);
        }

        public IEnumerable<Transaction> GetTransactions(User user, int count)
        {
            return _transactions.Where(i => i.User == user).Take(count);
        }

        public User GetUserByUsername(string username)
        {
            return _users.FirstOrDefault(i => i.Username == username);
        }

        public IEnumerable<User> GetUsers(Func<User, bool> predicate)
        {
            return _users.Where(predicate);
        }
    }
}
