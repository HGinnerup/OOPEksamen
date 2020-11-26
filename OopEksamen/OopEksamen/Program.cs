using OopEksamen.Classes;
using System;
using System.IO;

namespace OopEksamen
{
    class Program
    {
        static void Main(string[] args)
        {
            var visualStudioSolutionPath = @"..\..\..\..";
            using var stregSystem = new StregSystem(dataPath: Path.Join(visualStudioSolutionPath, @"data"));

            foreach (var product in stregSystem.ActiveProducts)
            {
                Console.WriteLine($"{product.ID}, {product.Name}, {product.Price}");
            }


            Console.WriteLine("Hello World!");
        }
    }
}
