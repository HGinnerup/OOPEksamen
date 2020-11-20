using System;
using System.Collections.Generic;
using System.Globalization;
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

        public override string ToString() {
            return (((decimal)Value) / 100).ToString("#.00 DKK", CultureInfo.CurrentCulture);
        }
    }
}
