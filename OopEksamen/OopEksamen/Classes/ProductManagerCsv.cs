using OopEksamen.Interfaces;
using OopEksamen.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OopEksamen.Classes
{
    class ProductManagerCsv : CsvManagerBase<Product>, IProductManager
    {
        public ProductManagerCsv(string filePath, char delimiter = ',', string newLine = null, Encoding encoding = null, uint headerLineCount = 1) : base(filePath, delimiter, newLine, encoding, headerLineCount)
        {
        }

        public string CsvPath { get; private set; }

        protected override string[] DataEncode(Product data)
        {
            return new string[] {
                data.ID.ToString(),
                data.Name,
                ((int)data.Price).ToString()
            };
        }
        protected override Product DataParse(string[] data)
        {
            return new Product()
            {
                ID = uint.Parse(data[0]),
                Name = data[1],
                Price = int.Parse(data[2])
            };
        }

        public IEnumerable<Product> Products => base.GetData();

        public void AddProduct(Product product)
        {
            base.AppendData(product);
        }

        public void DeleteProduct(string productID)
        {
            throw new NotImplementedException();
        }

        public uint GetAvailableID()
        {
            throw new NotImplementedException();
        }

        public Product GetProductByID(uint id)
        {
            throw new NotImplementedException();
        }

        public void UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
