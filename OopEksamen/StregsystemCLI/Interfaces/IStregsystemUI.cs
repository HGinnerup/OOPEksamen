﻿using OopEksamen.Models;
using OopEksamen.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Text;

namespace StregsystemCLI.Interfaces
{
    interface IStregsystemUI
    {
        void DisplayUserNotFound(string username);
        void DisplayProductNotFound(string product);
        void DisplayUserInfo(User user);
        void DisplayTooManyArgumentsError(string command);
        void DisplayAdminCommandNotFoundMessage(string adminCommand);
        void DisplayUserBuysProduct(BuyTransaction transaction);
        void DisplayUserBuysProduct(int count, BuyTransaction transaction);
        void Close();
        void DisplayInsufficientCash(User user, Product product);
        void DisplayGeneralError(string errorString);
        void Start();
        event StregsystemEvent CommandEntered;
    }
    delegate void StregsystemEvent(string rawString, string command, string[] args);
}
