using System;
using System.IO;
using OopEksamen.Classes;
using OopEksamen.Models;
using UpdateOldData.Classes;

namespace UpdateOldData
{
    /// <summary>
    /// Program meant only to run once while switching from the old system, to the new.
    /// Intended to convert the old data-formats, to the new.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            
            var visualStudioSolutionPath = @"..\..\..\..";
            using var stregSystem = new StregSystem(dataPath: Path.Join(visualStudioSolutionPath, @"data"));

            // Products
            using var productReader = new OldProductReader(Path.Join(visualStudioSolutionPath, @"originalData\products.csv"), delimiter: ';');
            foreach (var product in productReader.GetProducts())
            {
                stregSystem.ProductManager.AddProduct(product);
            }
            Console.WriteLine($"Finished appending old product-data");

            // Users
            using var userReader = new OldUserReader(Path.Join(visualStudioSolutionPath, @"originalData\users.csv"), delimiter: ',');
            foreach (var user in userReader.GetUsers())
            {
                stregSystem.UserManager.AddUser(user);
            }
            Console.WriteLine($"Finished appending old user-data");

        }
    }
}
