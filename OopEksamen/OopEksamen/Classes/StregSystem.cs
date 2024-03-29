﻿using OopEksamen.Classes.Csv;
using OopEksamen.Interfaces;
using OopEksamen.Models;
using OopEksamen.Models.Transactions;
using OopEksamen.Structs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OopEksamen.Classes
{
    public class StregSystem : IStregSystem
    {
        public IProductManager ProductManager { get; set; }
        public IUserManager UserManager { get; set; }
        public ITransactionManager TransactionManager { get; set; }
        private ActionLogger _transactionLogger { get; }


        public StregSystem(string logDirectory = "logs", string dataPath = "data")
        {
            ProductManager = new ProductManagerCsv(Path.Combine(dataPath, "products.csv"));
            ActiveProducts = ProductManager.Products.Where(i => i.Active);
            UserManager = new UserManagerCsv(Path.Combine(dataPath, "users.csv"));
            TransactionManager = new TransactionManagerCsv(Path.Combine(dataPath, "transactions.csv"), this);
            _transactionLogger = new ActionLogger(Path.Combine(logDirectory, "transactions.txt"));
        }

        public IEnumerable<Product> ActiveProducts { get; set; }

        public event UserBalanceNotification UserBalanceWarning;
        public BuyTransaction BuyProduct(User user, Product product)
        {
            var transaction = new BuyTransaction(TransactionManager.GetAvailableID(), user, product);

            if (user.Balance + transaction.Amount <= user.BalanceWarningThreshold)
                UserBalanceWarning(user, user.BalanceWarningThreshold);

            ExecuteTransaction(transaction);
            return transaction;
        }
        public MultiBuyTransaction BuyProduct(User user, Product product, uint count)
        {
            var transaction = new MultiBuyTransaction(TransactionManager.GetAvailableID(), user, product, count);

            if (user.Balance + transaction.Amount <= user.BalanceWarningThreshold)
                UserBalanceWarning(user, user.BalanceWarningThreshold);

            ExecuteTransaction(transaction);
            return transaction;
        }
        public InsertCashTransaction AddCreditsToAccount(User user, Money amount)
        {
            var transaction = new InsertCashTransaction(TransactionManager.GetAvailableID(), user, amount);
            ExecuteTransaction(transaction);
            return transaction;
        }

        private void ExecuteTransaction(Transaction transaction)
        {
            transaction.Execute();
            _transactionLogger.Log(transaction.ToString());
            TransactionManager.AddTransaction(transaction);
            UserManager.UpdateUser(transaction.User);
        }


        public Product GetProductByID(uint id)
        {
            return ProductManager.GetProductByID(id);
        }

        public IEnumerable<Transaction> GetTransactions(User user, int count)
        {
            return TransactionManager.GetTransactions(i => i.User.Equals(user)).Reverse().Take(count).ToList();
        }

        public User GetUserByUsername(string username)
        {
            return UserManager.GetUserByUsername(username);
        }

        public IEnumerable<User> GetUsers(Func<User, bool> predicate)
        {
            return UserManager.GetUsers(predicate);
        }

    }
}
