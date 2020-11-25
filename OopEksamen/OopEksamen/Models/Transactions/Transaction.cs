using OopEksamen.Structs;
using System;
using System.Collections.Generic;
using System.Text;

namespace OopEksamen.Models.Transactions
{
    public abstract class Transaction
    {
        public ulong ID { get; set; }
        public User User { get; set; }
        public DateTime Date { get; set; }
        /// <summary>
        /// The amount being added to @User's balance
        /// </summary>
        public Money Amount { get; set; }
        public Transaction(ulong id, User user, Money amount)
        {
            ID = id;
            User = user;
            Amount = amount;
        }

        public bool Executed { get; protected set; } = false;
        public abstract void Execute();

        public abstract override string ToString();

    }
}
