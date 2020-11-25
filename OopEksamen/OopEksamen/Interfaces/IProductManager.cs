using OopEksamen.Classes;
using OopEksamen.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OopEksamen.Interfaces
{
    interface IProductManager : IDisposable
    {
        public IEnumerable<Product> Products { get; }
        public Product GetProductByID(uint id);
        public uint GetAvailableID();
        public void AddProduct(Product product);
        public void UpdateProduct(Product product);
        public void DeleteProduct(string productID);
    }
}
