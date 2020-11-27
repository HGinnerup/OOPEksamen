using OopEksamen.Exceptions;
using OopEksamen.Interfaces;
using OopEksamen.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace OopEksamen.Classes.Csv
{
    class ProductManagerCsv : CsvManagerBase<Product>, IProductManager
    {
        public ProductManagerCsv(string filePath, char delimiter = ',', string newLine = null, Encoding encoding = null) : base(filePath, new string[] { "ID,Name,Active,Price,CanBeBoughtOnCredit,SeasonStartDate,SeasonEndDate" }, delimiter, newLine, encoding)
        {
        }

        public string CsvPath { get; private set; }

        protected override string[] DataEncode(Product data)
        {
            return new string[] {
                data.ID.ToString(),
                data.Name,
                data.Active.ToString(),
                ((decimal)data.Price).ToString(CultureInfo.InvariantCulture),
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
                price: decimal.Parse(data[3], CultureInfo.InvariantCulture),
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

        public void DeleteProduct(Product product)
        {
            ReWriteFileWithData(GetData().Where(i => !i.Equals(product)));
        }

        public uint GetAvailableID()
        {
            var ids = GetData().Select(i => i.ID).ToList();
            if (ids.Count == 0) return 1;
            else return ids.Max() + 1;
        }

        public Product GetProductByID(uint id)
        {
            var product = GetData().FirstOrDefault(i => i.ID == id);
            if (product == null) throw new ProductNotFoundException(id);
            return product;
        }

        public void UpdateProduct(Product product)
        {
            var data = GetData().ToList();
            var index = data.FindIndex(i => i.Equals(product));
            data[index] = product;

            ReWriteFileWithData(data);
        }
    }
}
