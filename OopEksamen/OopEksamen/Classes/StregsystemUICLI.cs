using OopEksamen.Models;
using OopEksamen.Models.Transactions;
using OopEksamen.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopEksamen.Structs;

namespace OopEksamen.Classes
{
    public class StregsystemUICLI : IStregsystemUI
    {
        public event StregsystemCommand CommandEntered;
        public bool Running { get; private set; } = false;

        private IStregSystem _stregsystem { get; set; }
        public StregsystemUICLI(IStregSystem stregSystem)
        {
            _stregsystem = stregSystem;
        }

        protected virtual string ReadLine() => Console.ReadLine(); // To enable unittest
        public void Start()
        {
            Console.WriteLine("Welcome to stregsystemCLI");
            Running = true;

            DisplayActiveProducts();

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

        public void DisplayProductNotFound(uint productID)
        {
            Console.WriteLine($"Cannot find product \"{productID}\"");
        }

        public void DisplayTooManyArgumentsError(string command)
        {
            Console.WriteLine($"Too many arguements in command: {command}");
        }

        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            Console.WriteLine(transaction);
        }

        public void DisplayUserInfo(User user, IEnumerable<Transaction> transactions)
        {
            Console.WriteLine(user);
            Console.WriteLine($"Balance: {user.Balance}");
            if(user.Balance < user.BalanceWarningThreshold)
            {
                Console.WriteLine("Warning: Balance under " + user.BalanceWarningThreshold);
            }
            Console.WriteLine("Recent transactions: ");
            foreach(var transaction in transactions)
            {
                Console.WriteLine("\t" + transaction);
            }
        }

        public void DisplayUserNotFound(string username)
        {
            Console.WriteLine($"Unknown user \"{username}\"");
        }

        public void DisplayActiveProducts()
        {
            DisplayProducts(_stregsystem.ActiveProducts);
        }
        public void DisplayProducts(IEnumerable<Product> products)
        {
            foreach (var product in _stregsystem.ActiveProducts)
            {
                if (product.Active)
                {
                    Console.WriteLine("{0}\t{1}\t{2}",
                        product.ID.ToString().PadLeft(5),
                        product.Price.ToString().PadLeft(10),
                        product.Name
                    );
                }
                else
                {
                    Console.WriteLine($"{0}\t(INACTIVE)\t{1}\t{2}",
                        product.ID.ToString().PadLeft(5),
                        product.Price.ToString().PadLeft(10),
                        product.Name
                    );
                }
            }
        }

        public void DisplayUserBalanceWarning(User user, Money threshold)
        {
            Console.WriteLine($"Balance less than {threshold}: {user}");
        }


    }
}
