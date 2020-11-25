using OopEksamen.Classes.Transactions;
using OopEksamen.Interfaces;
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
        private IProductManager _productManager { get; set; }
        private IUserManager _userManager { get; set; }
        private ITransactionManager _transactionManager { get; set; }
        private ActionLogger _transactionLogger { get; }


        public StregSystem(string logDirectory = "logs", string dataPath = "data")
        {
            _productManager = new ProductManagerCsv(Path.Combine(dataPath, "products.csv"));
            _userManager = new UserManagerCsv(Path.Combine(dataPath, "users.csv"));
            _transactionManager = new TransactionManagerCsv(Path.Combine(dataPath, "transactions.csv"));
            _transactionLogger = new ActionLogger(Path.Combine(logDirectory, "transactions.log"));
        }


        public IEnumerable<Product> ActiveProducts { get; set; } = new List<Product>();

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
            return _productManager.GetProductByID(id);
        }

        public IEnumerable<Transaction> GetTransactions(User user, int count)
        {
            return _transactionManager.GetTransactions(i => i.User.Equals(user)).Take(count);
        }

        public User GetUserByUsername(string username)
        {
            return _userManager.GetUserByUsername(username);
        }

        public IEnumerable<User> GetUsers(Func<User, bool> predicate)
        {
            return _userManager.GetUsers(predicate);
        }

        public void Dispose()
        {
            _productManager.Dispose();
            _userManager.Dispose();
            _transactionManager.Dispose();
            _transactionLogger.Dispose();
        }
    }
}
