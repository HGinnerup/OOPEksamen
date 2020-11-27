using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace OopEksamen.Structs
{
    public struct Money
    {
        private decimal Value { get; set; }

        public Money(decimal value) 
        {
            Value = value;
        }

        public static implicit operator Money(decimal value) => new Money(value);
        public static implicit operator decimal(Money currency) => currency.Value;

        public override string ToString() {
            //return (((decimal)Value) / 100).ToString("#.00 DKK", CultureInfo.CurrentCulture);

            var numberFormat = CultureInfo.CreateSpecificCulture("da-DK").NumberFormat;

            return Value.ToString("C2", numberFormat);
        }
    }
}
