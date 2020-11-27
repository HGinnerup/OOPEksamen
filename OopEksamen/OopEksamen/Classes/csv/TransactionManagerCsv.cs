using OopEksamen.Interfaces;
using OopEksamen.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopEksamen.Classes.Csv
{
    class TransactionManagerCsv : CsvManagerBase<Transaction>, ITransactionManager
    {

        private IStregSystem _stregSystem { get; set; }
        public TransactionManagerCsv(string filePath, IStregSystem stregSystem, char delimiter = ',', string newLine = null, Encoding encoding = null) : base(filePath, new string[] { "ID, UserID, Date, Amount" }, delimiter, newLine, encoding)
        {
            _stregSystem = stregSystem;
        }

        public IEnumerable<Transaction> Transactions => throw new NotImplementedException();

        public void AddTransaction(Transaction transaction)
        {
            AppendData(transaction);
        }

        public ulong GetAvailableID()
        {
            return GetData().Select(i => i.ID).Max() + 1;
        }

        public IEnumerable<Transaction> GetTransactions(Func<Transaction, bool> predicate)
        {
            return GetData().Where(predicate);
        }

        protected override string[] DataEncode(Transaction data)
        {
            if (!data.Executed) throw new InvalidOperationException("Transaction must be executed before being saved");

            if (data is InsertCashTransaction)
            {
                return new string[]
                {
                    nameof(InsertCashTransaction),
                    data.ID.ToString(),
                    data.User.Username,
                    data.Date.ToString(),
                    ((int)data.Amount).ToString()
                };
            }

            return new string[]
            {
                data.ID.ToString(),
                data.User.Username,
                data.Date.ToString(),
                ((int)data.Amount).ToString()
            };
        }

        protected override Transaction DataParse(string[] data)
        {
            var iD = uint.Parse(data[0]);
            var username = data[1];
            var date = DateTime.Parse(data[2]);
            var amount = int.Parse(data[3]);

            var user = _stregSystem.GetUserByUsername(username);

            return new Transaction(iD, user, amount, date);
        }
    }
}
