using OopEksamen.Exceptions;
using OopEksamen.Models;
using OopEksamen.Models.Transactions;
using OopEksamen.Structs;
using System;
using System.Collections.Generic;
using System.Text;

namespace OopEksamen.Interfaces
{
    public interface IStregsystemUI : IDisposable
    {
        void DisplayActiveProducts();
        void DisplayProducts(IEnumerable<Product> products);
        void DisplayUserNotFound(string username);
        void DisplayProductNotFound(uint productID);
        void DisplayUserInfo(User user, IEnumerable<Transaction> transactions);
        void DisplayTooManyArgumentsError(string command);
        void DisplayAdminCommandNotFoundMessage(string adminCommand);
        void DisplayUserBuysProduct(BuyTransaction transaction);  // Variation with count removed as parameter, as it should be part of the transaction-model
        void DisplayUserBalanceWarning(User user, Money threshold);
        void DisplayInsufficientCash(User user, Product product);
        void DisplayGeneralError(string errorString);
        void Start();
        event StregsystemCommand CommandEntered;
    }
}
