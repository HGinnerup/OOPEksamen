using OopEksamen.Classes;
using OopEksamen.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Text;

namespace OopEksamen.Interfaces
{
    public interface ITransactionManager
    {
        IEnumerable<Transaction> Transactions { get; }
        IEnumerable<Transaction> GetTransactions(Func<Transaction, bool> predicate);
        void AddTransaction(Transaction transaction);
        ulong GetAvailableID();
    }
}
