using System;
using System.IO;
using OopEksamen.Classes;
using OopEksamen.Models;
using UpdateOldData.Classes;

namespace UpdateOldData
{
    class Program
    {
        static void Main(string[] args)
        {
            // Update old data
            var visualStudioSolutionPath = @"..\..\..\..";
            var productReader = new OldProductReader(Path.Join(visualStudioSolutionPath, @"originalData\products.csv"), delimiter: ';');
            var stregSystem = new StregSystem(dataPath: Path.Join(visualStudioSolutionPath, @"data"));
            foreach (var product in productReader.GetProducts())
            {
                stregSystem.ProductManager.AddProduct(product);
            }

            Console.WriteLine("Hello World!");
        }
    }
}
