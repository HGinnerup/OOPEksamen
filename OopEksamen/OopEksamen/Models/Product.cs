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
            if (Active)
            {
                return string.Format("{0}\t{1}\t{2}",
                    ID.ToString().PadLeft(5),
                    Price.ToString().PadLeft(10),
                    Name
                );
            }
            else
            {
                return string.Format($"{0}\t(INACTIVE)\t{1}\t{2}",
                    ID.ToString().PadLeft(5),
                    Price.ToString().PadLeft(10),
                    Name
                );
            }

        }
    }
}
