using System;
using System.Collections.Generic;
using System.Text;
using OopEksamen.Structs;

namespace OopEksamen.Classes
{
    public class User : IComparable
    {
        public User(uint iD, string firstname, string lastname, string username, string email)
        {
            ID = iD;
            Firstname = firstname;
            Lastname = lastname;
            Username = username;
            Email = email;
        }

        public uint ID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int Balance { get; set; } = 0;
        public Money Credit { get; set; } = 0;



        public override string ToString()
        {
            return $"{Firstname} {Lastname} ({Email})";
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != this.GetType()) return false;
            // obj is an instance of User

            return (obj as User).ID == ID;
        }

        public override int GetHashCode()
        {
            return (int)ID;
        }

        public int CompareTo(object obj)
        {
            return ID.CompareTo(obj);
        }
    }
}
