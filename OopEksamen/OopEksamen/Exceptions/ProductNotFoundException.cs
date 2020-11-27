using System;
using System.Collections.Generic;
using System.Text;

namespace OopEksamen.Exceptions
{
    public class ProductNotFoundException : ItemNotFoundException {
        public ProductNotFoundException(string product)
        {
            Product = product;
        }

        public string Product { get; set; }
    }
}
