using OopEksamen.Interfaces;
using OopEksamen.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OopEksamen.Classes.Csv
{
    class UserManagerCsv : CsvManagerBase<User>, IUserManager
    {
        public UserManagerCsv(string filePath, char delimiter = ',', string newLine = null, Encoding encoding = null, uint headerLineCount = 1) : base(filePath, delimiter, newLine, encoding, headerLineCount)
        {
        }

        public IEnumerable<User> Users => throw new NotImplementedException();

        public void AddUser(User user)
        {
            base.AppendData(user);
        }

        public void DeleteUser(string username)
        {
            throw new NotImplementedException();
        }

        public uint GetAvailableID()
        {
            throw new NotImplementedException();
        }

        public User GetUserByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetUsers(Func<User, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        protected override string[] DataEncode(User data)
        {
            return new string[]
            {
                data.ID.ToString(),
                data.Firstname,
                data.Lastname,
                data.Username,
                data.Email,
                ((int)data.Balance).ToString(),
                ((int)data.Credit).ToString()
            };
        }

        protected override User DataParse(string[] data)
        {
            return new User(
                iD: uint.Parse(data[0]),
                firstname: data[1],
                lastname: data[2],
                username: data[3],
                email: data[4]
                )
            {
                Balance = int.Parse(data[5]),
                Credit = int.Parse(data[6])
            };
        }
    }
}
