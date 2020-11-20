using System;
using System.Collections.Generic;
using System.Text;

namespace OopEksamen.Classes
{
    public class Product
    {
        public uint ID { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public int Price { get; set; }
        public bool CanBeBoughtOnCredit { get; set; }
        public DateTime SeasonStartDate { get; set; }
        public DateTime SeasonEndDate { get; set; }
    }
}
