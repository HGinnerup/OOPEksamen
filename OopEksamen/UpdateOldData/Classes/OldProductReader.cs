using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using OopEksamen.Classes;
using OopEksamen.Classes.Csv;
using OopEksamen.Models;

namespace UpdateOldData.Classes
{
    class OldProductReader : CsvManagerBase<Product>
    {
        public OldProductReader(string filePath, char delimiter = ',', string newLine = null, Encoding encoding = null) : base(filePath, new string[] { "" }, delimiter, newLine, encoding)
        {
        }

        protected override string[] DataEncode(Product data)
        {
            throw new NotImplementedException();
        }

        private string stripHtml(string str)
        {
            return (new Regex("<[^>]+>")).Replace(str, string.Empty);
        }

        protected override Product DataParse(string[] data)
        {
            // Columns in original .csv
            var id = uint.Parse(data[0]);
            var name = stripHtml(data[1]);
            var price = int.Parse(data[2]);
            var deactivate_date = data[3] != "0";

            return new Product(
                iD: id,
                name: name,
                active: true,
                price: price,
                canBeBoughtOnCredit: true,
                seasonStartDate: null,
                seasonEndDate: null
            );
        }

        public IEnumerable<Product> GetProducts() => GetData(); 
    }
}
