using OopEksamen.Classes;
using OopEksamen.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OopEksamen.Interfaces
{
    public interface IProductManager : IDisposable
    {
        public IEnumerable<Product> Products { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="Exceptions.ProductNotFoundException"/>
        /// <returns></returns>
        public Product GetProductByID(uint id);
        public uint GetAvailableID();
        public void AddProduct(Product product);
        public void UpdateProduct(Product product);
        public void DeleteProduct(Product product);
    }
}
