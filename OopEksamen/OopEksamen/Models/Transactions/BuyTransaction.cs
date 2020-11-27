using OopEksamen.Exceptions;
using OopEksamen.Structs;
using System;
using System.Collections.Generic;
using System.Text;

namespace OopEksamen.Models.Transactions
{
    public class BuyTransaction : Transaction
    {
        public BuyTransaction(ulong id, User user, Product product) : base(id, user, -product.Price)
        {
            Product = product;
            Amount = product.Price;
        }
        public BuyTransaction(ulong id, User user, Money amount, Product product) : base(id, user, amount)
        {
            Product = product;
        }

        public Product Product { get; set; }

        /// <summary>
        /// Finalizes the transaction
        /// </summary>
        /// <exception cref="InsufficientCreditsException" />
        /// <exception cref="InvalidOperationException" />
        public override void Execute()
        {
            if (Executed) throw new InvalidOperationException("Transaction has already been executed");
            if (!Product.Active) throw new InvalidOperationException("Cannot buy inactive product");

            if(Product.CanBeBoughtOnCredit)
            {
                if (User.Balance + User.Credit < Amount) throw new InsufficientCreditsException(User, Product);
            }
            else
            {
                if (User.Balance < Amount) throw new InsufficientCreditsException(User, Product);
            }
            Date = DateTime.Now;

            User.Balance -= Amount;
            Executed = true;
        }

        public override string ToString()
        {
            return $"{User} bought {Product} for {Amount}";
        }
    }
}
