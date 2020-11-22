using System;
using System.Collections.Generic;
using System.Text;
using OopEksamen.Structs;
using OopEksamen.Utilities;

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

        public uint ID { get; private set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                if (Regexes.Email.IsMatch(value)) _email = value;
                else throw new ArgumentException($@"Invalid email ""{_email}""");
            }
        }
        public Money Balance { get; set; } = 0;
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
