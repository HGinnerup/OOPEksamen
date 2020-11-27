using System;
using System.Collections.Generic;
using System.Text;
using OopEksamen.Exceptions;
using OopEksamen.Interfaces;
using OopEksamen.Models.Transactions;

namespace OopEksamen.Classes
{
    public class StregsystemController : IDisposable
    {
        public StregsystemController(IStregSystem stregSystem, IStregsystemUI stregSystemUI)
        {
            StregSystem = stregSystem;
            StregSystemUI = stregSystemUI;
            _LoadCommands();
            StregSystemUI.CommandEntered += CommandEvent;


            var user = StregSystem.GetUserByUsername("Username");
            StregSystem.GetTransactions(user, 10); 

        }

        event StregsystemCommand CommandEvent;

        void ParseCommand(string rawString, string command, string[] args)
        {
            
            if(command[0] == ':') // Admin-commands
            {
                StregsystemCommand func;
                if (_commands.ContainsKey((command, args.Length))) func = _commands[(command, args.Length)];
                else if (_commands.ContainsKey((command, null))) func = _commands[(command, null)];
                else
                {
                    StregSystemUI.DisplayAdminCommandNotFoundMessage(command);
                    return;
                }

                try
                {
                    func(rawString, command, args);
                }
                catch(ProductNotFoundException e)
                {
                }
                catch(Exception e)
                {
                    StregSystemUI.DisplayGeneralError(e.Message);
                }
            }
            else // Default
            {
                
            }
        }

        private void _LoadCommands()
        {
            CommandEvent += ParseCommand;

            AddCommand(
                new string[] { ":q", ":quit" },
                (raw, cmd, args) =>
                {
                    StregSystemUI.Dispose();
                    StregSystem.Dispose();
                }
            );

            AddCommand(
                ":activate",
                (raw, cmd, args) =>
                {
                    var productId = uint.Parse(args[0]);
                    var product = StregSystem.ProductManager.GetProductByID(productId);
                    product.Active = true;
                    StregSystem.ProductManager.UpdateProduct(product);
                },
                argCount: 1
            );

            AddCommand(
                ":deactivate",
                (raw, cmd, args) =>
                {
                    var productId = uint.Parse(args[0]);
                    var product = StregSystem.ProductManager.GetProductByID(productId);
                    product.Active = false;
                    StregSystem.ProductManager.UpdateProduct(product);
                },
                argCount: 1
            );


            AddCommand(
                ":crediton",
                (raw, cmd, args) =>
                {
                    var productId = uint.Parse(args[0]);
                    var product = StregSystem.ProductManager.GetProductByID(productId);
                    product.CanBeBoughtOnCredit = true;
                    StregSystem.ProductManager.UpdateProduct(product);
                },
                argCount: 1
            );

            AddCommand(
                ":creditoff",
                (raw, cmd, args) =>
                {
                    var productId = uint.Parse(args[0]);
                    var product = StregSystem.ProductManager.GetProductByID(productId);
                    product.CanBeBoughtOnCredit = false;
                    StregSystem.ProductManager.UpdateProduct(product);
                },
                argCount: 1
            );

            AddCommand(
                ":addcredits",
                (raw, cmd, args) =>
                {
                    var userId = args[0];
                    var credits = int.Parse(args[1]);

                    var user = StregSystem.UserManager.GetUserByUsername(userId);

                    StregSystem.AddCreditsToAccount(user, credits);
                },
                argCount: 2
            );



        }

        private Dictionary<ValueTuple<string, int?>, StregsystemCommand> _commands { get; set; } = new Dictionary<ValueTuple<string, int?>, StregsystemCommand>();
        public void AddCommand(string cmd, StregsystemCommand func, int? argCount = null)
        {
            _commands.Add(new ValueTuple<string, int?>(cmd, argCount), func);
        }
        public void AddCommand(string[] cmdAliases, StregsystemCommand func, int? argCount = null)
        {
            foreach (var alias in cmdAliases) AddCommand(alias, func, argCount);
        }

        public void Dispose()
        {
            StregSystem.Dispose();
            StregSystemUI.Dispose();
        }

        IStregSystem StregSystem { get; set; }
        IStregsystemUI StregSystemUI { get; set; }


    }
}
