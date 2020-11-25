using System;
using System.Collections.Generic;
using System.Text;

namespace OopEksamen.Models.Transactions
{
    /// <summary>
    /// The exception that is thrown when the balance of a User is too low to execute a transaction
    /// </summary>
    public class InsufficientCreditsException : Exception
    {
        public User User { get; set; }
        public Product Product { get; set; }

        public InsufficientCreditsException(User user, Product product) : base($"{user}'s balance ({user.Balance}) is too low to pay for {product}")
        {
            User = user;
            Product = product;
        }
    }
}
