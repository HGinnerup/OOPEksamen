using OopEksamen.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace OopEksamen.Interfaces
{
    interface IProductManager
    {
        IEnumerable<Product> Products { get; }
        Product GetProductByID(uint id);
        uint GetAvailableID();
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(string productID);
    }
}
