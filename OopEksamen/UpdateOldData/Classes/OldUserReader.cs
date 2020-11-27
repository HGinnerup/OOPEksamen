using OopEksamen.Classes;
using OopEksamen.Classes.Csv;
using OopEksamen.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace UpdateOldData.Classes
{
    class OldUserReader : CsvManagerBase<User>
    {
        public OldUserReader(string filePath, char delimiter = ',', string newLine = null, Encoding encoding = null) : base(filePath, new string[] { "" }, delimiter, newLine, encoding)
        {
        }

        protected override string[] DataEncode(User data)
        {
            throw new NotImplementedException();
        }

        protected override User DataParse(string[] data)
        {
            var id = uint.Parse(data[0]);
            var firstname = data[1];
            var lastname = data[2];
            var username = data[3];
            var balance = decimal.Parse(data[4]) / 100;
            var email = data[5];

            return new User(id, firstname, lastname, username, email) { Balance = balance };
        }

        public IEnumerable<User> GetUsers() => GetData();
    }
}
