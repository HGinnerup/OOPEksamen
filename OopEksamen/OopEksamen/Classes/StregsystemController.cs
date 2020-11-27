using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopEksamen.Exceptions;
using OopEksamen.Interfaces;
using OopEksamen.Models;
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
            StregSystem.UserBalanceWarning += StregSystemUI.DisplayUserBalanceWarning;
        }
        event StregsystemCommand CommandEvent;

        void ParseCommand(string rawString, string command, string[] args)
        {
            try
            {
                if (command[0] == ':') // Admin-commands
                {
                    StregsystemCommand func;
                    if (_commands.ContainsKey((command, args.Length))) func = _commands[(command, args.Length)];
                    else if (_commands.ContainsKey((command, null))) func = _commands[(command, null)];
                    else throw new AdminCommandNotFoundException();

                    func(rawString, command, args);
                }
                else // Default
                {
                    var user = StregSystem.GetUserByUsername(command);
                    Product product;
                    BuyTransaction transaction;

                    switch (args.Length)
                    {
                        case 0:
                            StregSystemUI.DisplayUserInfo(user, StregSystem.GetTransactions(user, 10));
                            break;
                        case 1:
                            product = StregSystem.GetProductByID(uint.Parse(args[0]));

                            transaction = StregSystem.BuyProduct(user, product);
                            StregSystemUI.DisplayUserBuysProduct(transaction);
                            break;
                        case 2:
                            var count = uint.Parse(args[0]);
                            product = StregSystem.GetProductByID(uint.Parse(args[1]));

                            transaction = StregSystem.BuyProduct(user, product);
                            StregSystemUI.DisplayUserBuysProduct(transaction);
                            break;
                        default:
                            throw new TooManyArgumentsException();
                    }

                }
            }
            catch (TooManyArgumentsException e)
            {
                StregSystemUI.DisplayTooManyArgumentsError(e.Message);
            }
            catch (UserNotFoundException e)
            {
                StregSystemUI.DisplayUserNotFound(e.Message);
            }
            catch (ProductNotFoundException e)
            {
                StregSystemUI.DisplayProductNotFound(e.ProductID);
            }
            catch (Exception e)
            {
                StregSystemUI.DisplayGeneralError(e.Message);
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
