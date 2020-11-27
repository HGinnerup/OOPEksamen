using System;
using System.Collections.Generic;
using System.Text;

namespace OopEksamen.Exceptions
{
    public class UserNotFoundException : ItemNotFoundException {
        public UserNotFoundException(string username)
        {
            Username = username;
        }

        public string Username { get; set; }
    }
}
