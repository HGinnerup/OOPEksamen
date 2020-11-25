using OopEksamen.Interfaces;
using OopEksamen.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OopEksamen.Classes
{
    class UserManagerCsv : IUserManager
    {
        public UserManagerCsv(string csvPath)
        {

        }
        public IEnumerable<User> Users => throw new NotImplementedException();

        public void AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(string username)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
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
    }
}
