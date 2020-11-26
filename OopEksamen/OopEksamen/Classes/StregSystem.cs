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
    public class StregSystem : IStregSystem, IDisposable
    {
        public IProductManager ProductManager { get; set; }
        public IUserManager UserManager { get; set; }
        public ITransactionManager TransactionManager { get; set; }
        private ActionLogger _transactionLogger { get; }


        public StregSystem(string logDirectory = "logs", string dataPath = "data")
        {
            ProductManager = new ProductManagerCsv(Path.Combine(dataPath, "products.csv"), delimiter: ';');
            ActiveProducts = ProductManager.Products; //.Where(i => i.Active);
            UserManager = new UserManagerCsv(Path.Combine(dataPath, "users.csv"));
            TransactionManager = new TransactionManagerCsv(Path.Combine(dataPath, "transactions.csv"));
            _transactionLogger = new ActionLogger(Path.Combine(logDirectory, "transactions.log"));
        }


        public IEnumerable<Product> ActiveProducts { get; set; }

        public event UserBalanceNotification UserBalanceWarning;

        private uint _nextTransactionId { get; set; } = 0;
        private uint getNewTransactionID() => _nextTransactionId++;
        public BuyTransaction BuyProduct(User user, Product product)
        {
            var transaction = new BuyTransaction(getNewTransactionID(), user, product);
            ExecuteTransaction(transaction);
            return transaction;
        }
        public InsertCashTransaction AddCreditsToAccount(User user, Money amount)
        {
            var transaction = new InsertCashTransaction(getNewTransactionID(), user, amount);
            ExecuteTransaction(transaction);
            return transaction;
        }

        private void ExecuteTransaction(Transaction transaction)
        {
            _transactionLogger.Log(transaction.ToString());
            transaction.Execute();
        }


        public Product GetProductByID(uint id)
        {
            return ProductManager.GetProductByID(id);
        }

        public IEnumerable<Transaction> GetTransactions(User user, int count)
        {
            return TransactionManager.GetTransactions(i => i.User.Equals(user)).Take(count);
        }

        public User GetUserByUsername(string username)
        {
            return UserManager.GetUserByUsername(username);
        }

        public IEnumerable<User> GetUsers(Func<User, bool> predicate)
        {
            return UserManager.GetUsers(predicate);
        }

        public void Dispose()
        {
            ProductManager.Dispose();
            UserManager.Dispose();
            TransactionManager.Dispose();
            _transactionLogger.Dispose();
        }
    }
}
