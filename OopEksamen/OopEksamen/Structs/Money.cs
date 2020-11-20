using System;
using System.Collections.Generic;
using System.Text;

namespace OopEksamen.Structs
{
    public struct Money
    {
        private int Value { get; set; }

        public Money(int value) 
        {
            Value = value;
        }

        public static implicit operator Money(int value) => new Money(value);
        public static implicit operator int(Money currency) => currency.Value;

        public override string ToString() => $"{((decimal)Value) / 100} DKK";
    }
}
