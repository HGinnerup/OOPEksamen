using OopEksamen.Classes;
using OopEksamen.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Text;

namespace OopEksamen.Interfaces
{
    public interface ITransactionManager : IDisposable
    {
        IEnumerable<Transaction> Transactions { get; }
        IEnumerable<Transaction> GetTransactions(Func<Transaction, bool> predicate);
        void AddTransaction(Transaction product);
        ulong GetAvailableID();
    }
}
