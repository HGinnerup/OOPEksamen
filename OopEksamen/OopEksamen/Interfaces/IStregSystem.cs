using OopEksamen.Models;
using OopEksamen.Models.Transactions;
using OopEksamen.Structs;
using System;
using System.Collections.Generic;
using System.Text;

namespace OopEksamen.Interfaces
{
    public interface IStregSystem
    {
        public IProductManager ProductManager { get; set; }
        public IUserManager UserManager { get; set; }
        public ITransactionManager TransactionManager { get; set; }

        IEnumerable<Product> ActiveProducts { get; }
        InsertCashTransaction AddCreditsToAccount(User user, Money amount);
        BuyTransaction BuyProduct(User user, Product product);
        Product GetProductByID(uint id);
        IEnumerable<Transaction> GetTransactions(User user, int count);
        IEnumerable<User> GetUsers(Func<User, bool> predicate); // I assume GetUsers (plural) was intended to return a set of users.
        User GetUserByUsername(string username);
        event UserBalanceNotification UserBalanceWarning;
    }
    public delegate void UserBalanceNotification(User user, int threshold);
}
