using OopEksamen.Classes;
using OopEksamen.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OopEksamen.Interfaces
{
    interface IUserManager : IDisposable
    {
        IEnumerable<User> Users { get; }
        User GetUserByUsername(string username);
        IEnumerable<User> GetUsers(Func<User, bool> predicate);
        uint GetAvailableID();
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(string username);

    }
}
