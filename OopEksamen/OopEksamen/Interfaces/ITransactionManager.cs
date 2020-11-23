using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace OopEksamen.Interfaces
{
    public interface ITransactionManager
    {
        IEnumerable<Transaction> Transactions { get; }
        IEnumerable<Transaction> GetTransactions(Func<Transaction, bool> predicate);
        void AddTransaction(Transaction product);
        ulong GetAvailableID();
    }
}
