using OopEksamen.Models;
using OopEksamen.Models.Transactions;
using OopEksamen.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopEksamen.Classes
{
    public class StregsystemUICLI : IStregsystemUI
    {
        public event StregsystemCommand CommandEntered;
        public bool Running { get; private set; } = false;

        protected virtual string ReadLine() => Console.ReadLine(); // To enable unittest
        public void Start()
        {
            Console.WriteLine("Welcome to stregsystemCLI");
            Running = true;
            while (Running)
            {
                var rawString = ReadLine();
                var commandSplit = Utilities.StringHandling.SplitString(rawString, ' ').ToArray();

                CommandEntered(rawString, commandSplit.First(), commandSplit.Skip(1).ToArray());
            }
        }


        public void Dispose()
        {
            Console.WriteLine("Stregsystem closing down");
            Running = false;
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
            if (product.CanBeBoughtOnCredit && user.Credit > 0)
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


    }
}
