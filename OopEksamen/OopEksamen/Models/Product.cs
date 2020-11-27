using OopEksamen.Structs;
using System;
using System.Collections.Generic;
using System.Text;

namespace OopEksamen.Models
{
    public class Product
    {
        public Product(uint iD, string name, bool active, Money price, bool canBeBoughtOnCredit, DateTime? seasonStartDate, DateTime? seasonEndDate)
        {
            ID = iD;
            Name = name;
            Active = active;
            Price = price;
            CanBeBoughtOnCredit = canBeBoughtOnCredit;
            SeasonStartDate = seasonStartDate;
            SeasonEndDate = seasonEndDate;
        }

        public uint ID { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public Money Price { get; set; }
        public bool CanBeBoughtOnCredit { get; set; }
        public DateTime? SeasonStartDate { get; set; }
        public DateTime? SeasonEndDate { get; set; }

        public override string ToString()
        {
            return $"[{ID}] {Name} ({Price})";
        }
    }
}
