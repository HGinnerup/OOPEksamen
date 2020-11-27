using OopEksamen.Interfaces;
using OopEksamen.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Text;

namespace OopEksamen.Classes.Csv
{
    class TransactionManagerCsv : ITransactionManager
    {
        public TransactionManagerCsv(string csvPath)
        {

        }

        public IEnumerable<Transaction> Transactions => throw new NotImplementedException();

        public void AddTransaction(Transaction product)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public ulong GetAvailableID()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Transaction> GetTransactions(Func<Transaction, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
