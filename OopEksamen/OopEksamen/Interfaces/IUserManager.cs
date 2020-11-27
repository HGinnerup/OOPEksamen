using OopEksamen.Classes;
using OopEksamen.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OopEksamen.Interfaces
{
    public interface IUserManager
    {
        IEnumerable<User> Users { get; }
        User GetUserByUsername(string username);
        IEnumerable<User> GetUsers(Func<User, bool> predicate);
        uint GetAvailableID();
        void AddUser(User user);
        void UpdateUser(User user);
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="UserNotFoundException" />
        /// <param name="username"></param>
        void DeleteUser(string username);

    }
}
