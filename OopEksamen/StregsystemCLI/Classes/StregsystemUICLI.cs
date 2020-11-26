using OopEksamen.Models;
using OopEksamen.Models.Transactions;
using StregsystemCLI.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StregsystemCLI.Classes
{
    class StregsystemUICLI : IStregsystemUI
    {
        public event StregsystemEvent CommandEntered;

        public void Close()
        {
            Console.WriteLine("Stregsystem closing down");
        }

        public void DisplayAdminCommandNotFoundMessage(string adminCommand)
        {
            Console.WriteLine($"Unkwown command: \"{adminCommand}\"");
        }

        public void DisplayGeneralError(string errorString)
        {
            Console.WriteLine(errorString);
        }

        public void DisplayInsufficientCash(User user, Product product)
        {
            if(product.CanBeBoughtOnCredit && user.Credit > 0)
            {
                Console.WriteLine($"Insufficient funds: {user.Username} has {user.Balance} and credit {user.Credit}, and cannot buy {product.Name} for {product.Price}");
            }
            else
            {
                Console.WriteLine($"Insufficient funds: {user.Username} has {user.Balance} and cannot buy {product.Name} for {product.Price}");
            }
            
        }

        public void DisplayProductNotFound(string product)
        {
            Console.WriteLine($"Cannot find product \"{product}\"");
        }

        public void DisplayTooManyArgumentsError(string command)
        {
            Console.WriteLine($"Too many arguements in command: {command}");
        }

        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            Console.WriteLine(transaction);
        }

        public void DisplayUserInfo(User user)
        {
            Console.WriteLine(user);
        }

        public void DisplayUserNotFound(string username)
        {
            Console.WriteLine($"Unknown user \"{username}\"");
        }

        public void Start()
        {
            Console.WriteLine("Welcome to stregsystemCLI");
        }
    }
}
