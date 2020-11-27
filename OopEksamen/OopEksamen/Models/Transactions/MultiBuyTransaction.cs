using OopEksamen.Exceptions;
using OopEksamen.Structs;
using System;
using System.Collections.Generic;
using System.Text;

namespace OopEksamen.Models.Transactions
{
    public class MultiBuyTransaction : BuyTransaction
    {
        public MultiBuyTransaction(ulong id, User user, Product product, uint count) : base(id, user, product)
        {
            Product = product;
            Amount = (int)(count * product.Price);
            Count = count;
        }

        public uint Count { get; set; }

        public override string ToString()
        {
            return $"{User} bought {Count}x {Product} for {Amount}";
        }
    }
}
