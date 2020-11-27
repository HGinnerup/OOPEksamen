using OopEksamen.Structs;
using System;
using System.Collections.Generic;
using System.Text;

namespace OopEksamen.Models.Transactions
{
    public class InsertCashTransaction : Transaction
    {
        public InsertCashTransaction(ulong id, User user, Money amount) : base(id, user, amount)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="Exceptions.InsufficientCreditsException" />
        /// <exception cref="InvalidOperationException" />
        public override void Execute()
        {
            if (Executed) throw new InvalidOperationException("Transaction has already been executed");
            if (Amount < 0 && User.Balance - Amount < 0) throw new InvalidOperationException("Unsufficient Balance");
            Executed = true;
            User.Balance += Amount;
        }

        public override string ToString()
        {
            if(Amount >= 0)
            {
                return $"{User.Username} deposited {Amount}";
            }
            else
            {
                return $"{User.Username} withdrew {-Amount}"; //- to avoid double negative
            }
        }
    }
}
