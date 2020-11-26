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
                data.Active.ToString(),
                ((int)data.Price).ToString(),
                data.CanBeBoughtOnCredit.ToString(),
                data.SeasonStartDate.ToString(),
                data.SeasonEndDate.ToString()
            };
        }
        protected override Product DataParse(string[] data)
        {
            return new Product(
                iD: uint.Parse(data[0]),
                name: data[1],
                active: bool.Parse(data[2]),
                price: int.Parse(data[3]),
                canBeBoughtOnCredit: bool.Parse(data[4]),
                seasonStartDate: (data[5] != string.Empty) ? (DateTime?)DateTime.Parse(data[5]) : null,
                seasonEndDate: (data[6] != string.Empty) ? (DateTime?)DateTime.Parse(data[6]) : null
            );
        }

        public IEnumerable<Product> Products { get { return GetData(); } }

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
