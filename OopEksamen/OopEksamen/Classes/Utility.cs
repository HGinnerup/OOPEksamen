using System;
using System.Collections.Generic;
using System.Text;

namespace OopEksamen.Classes
{
    public static class Utility
    {
        public static string FormatCredits(int balance)
        {
            return $"{((decimal)balance) / 100} DKK";
        }
    }
}
