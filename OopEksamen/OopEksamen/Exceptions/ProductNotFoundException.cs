using System;
using System.Collections.Generic;
using System.Text;

namespace OopEksamen.Exceptions
{
    public class ProductNotFoundException : ItemNotFoundException {
        public ProductNotFoundException(uint productID)
        {
            ProductID = productID;
        }

        public uint ProductID { get; set; }
    }
}
