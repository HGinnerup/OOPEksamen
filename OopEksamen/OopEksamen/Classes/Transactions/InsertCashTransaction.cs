﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OopEksamen.Classes.Transactions
{
    public class InsertCashTransaction : Transaction
    {


        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="InsufficientCreditsException" />
        /// <exception cref="InvalidOperationException" />
        public override void Execute()
        {
            if (Executed) throw new InvalidOperationException("Transaction has already been executed");
            if (Amount < 0 && User.Balance - Amount < 0) throw new InvalidOperationException("Unsufficient Balance");
            Executed = true;
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}