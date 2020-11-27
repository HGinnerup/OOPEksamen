using OopEksamen.Interfaces;
using OopEksamen.Models;
using OopEksamen.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public void AddTransaction(Transaction transaction)
        {
            AppendData(transaction);
        }

        public ulong GetAvailableID()
        {
            var ids = GetData().Select(i => i.ID).ToList();
            if (ids.Count == 0) return 1;
            else return ids.Max() + 1;
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
                    ((decimal)data.Amount).ToString(CultureInfo.InvariantCulture)
                };
            }
            else if (data is MultiBuyTransaction)
            {
                return new string[]
                {
                    nameof(MultiBuyTransaction),
                    data.ID.ToString(),
                    data.User.Username,
                    data.Date.ToString(),
                    ((decimal)data.Amount).ToString(CultureInfo.InvariantCulture),
                    (data as MultiBuyTransaction).Product.ID.ToString(),
                    (data as MultiBuyTransaction).Count.ToString()
                };
            }
            else if (data is BuyTransaction)
            {
                
                return new string[]
                {
                    nameof(BuyTransaction),
                    data.ID.ToString(),
                    data.User.Username,
                    data.Date.ToString(),
                    ((decimal)data.Amount).ToString(CultureInfo.InvariantCulture),
                    (data as BuyTransaction).Product.ID.ToString()
                };
            }

            else throw new FormatException($"Unknown transaction-type of \"{data}\"");
        }

        protected override Transaction DataParse(string[] data)
        {
            var type = data[0];
            var iD = uint.Parse(data[1]);
            var username = data[2];
            var date = DateTime.Parse(data[3]);
            var amount = decimal.Parse(data[4], CultureInfo.InvariantCulture);

            var user = _stregSystem.GetUserByUsername(username);

            Product product;
            uint count;

            switch(type)
            {
                case nameof(InsertCashTransaction):
                    return new InsertCashTransaction(iD, user, amount) { Executed = true };
                case nameof(BuyTransaction):
                    product = _stregSystem.GetProductByID(uint.Parse(data[5]));
                    return new BuyTransaction(iD, user, amount, product) { Executed = true };
                case nameof(MultiBuyTransaction):
                    product = _stregSystem.GetProductByID(uint.Parse(data[5]));
                    count = uint.Parse(data[6]);
                    return new MultiBuyTransaction(iD, user, product, count);
                default:
                    throw new FormatException($"Unknown transaction-type {type}");
            }
        }
    }
}
