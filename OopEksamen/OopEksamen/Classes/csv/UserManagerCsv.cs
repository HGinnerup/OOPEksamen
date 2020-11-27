using OopEksamen.Exceptions;
using OopEksamen.Interfaces;
using OopEksamen.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace OopEksamen.Classes.Csv
{
    public class UserManagerCsv : CsvManagerBase<User>, IUserManager
    {
        public UserManagerCsv(string filePath, char delimiter = ',', string newLine = null, Encoding encoding = null) : base(filePath,new string[] { "ID,Firstname,Lastname,username,Email,Balance,Credit" }, delimiter, newLine, encoding) {}

        public IEnumerable<User> Users => GetData();

        public void AddUser(User user)
        {
            base.AppendData(user);
        }

        public void DeleteUser(string username)
        {
            var user = GetUserByUsername(username);
            ReWriteFileWithData(GetData().Where(i => !i.Equals(user)));
        }

        public uint GetAvailableID()
        {
            return GetData().Select(i => i.ID).Max() + 1;
        }

        public User GetUserByUsername(string username)
        {
            var user = GetData().FirstOrDefault(i => i.Username == username);
            if (user == null) throw new UserNotFoundException(username);
            return user;
        }

        public IEnumerable<User> GetUsers()
        {
            return GetData();
        }

        public IEnumerable<User> GetUsers(Func<User, bool> predicate)
        {
            return GetUsers().Where(predicate);
        }

        public void UpdateUser(User user)
        {
            var data = GetData().ToList();
            var index = data.FindIndex(i => i.Equals(user));
            data[index] = user;

            ReWriteFileWithData(data);
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
                ((decimal)data.Balance).ToString(CultureInfo.InvariantCulture),
                ((decimal)data.Credit).ToString(CultureInfo.InvariantCulture)
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
                Balance = decimal.Parse(data[5], CultureInfo.InvariantCulture),
                Credit = decimal.Parse(data[6], CultureInfo.InvariantCulture)
            };
        }
    }
}
