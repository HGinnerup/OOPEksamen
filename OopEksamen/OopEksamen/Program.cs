using OopEksamen.Classes;
using OopEksamen.Interfaces;
using System;
using System.IO;

namespace OopEksamen
{
    class Program
    {
        static void Main(string[] args)
        {
            var visualStudioSolutionPath = @"..\..\..\..";
            using IStregSystem stregSystem = new StregSystem(dataPath: Path.Join(visualStudioSolutionPath, @"data"));
            using IStregsystemUI stregSystemUI = new StregsystemUICLI();




            //foreach (var product in stregSystem.ActiveProducts)
            //{
            //    Console.WriteLine($"{product.ID}, {product.Name}, {product.Price}");
            //}


            Console.WriteLine("Hello World!");
        }
    }
}
