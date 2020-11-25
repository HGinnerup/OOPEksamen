using OopEksamen.Classes;
using System;

namespace OopEksamen
{
    class Program
    {
        static void Main(string[] args)
        {
            var stregSystem = new StregSystem();

            foreach(var product in stregSystem.ActiveProducts)
            {
                Console.WriteLine($"{product.ID}, {product.Name}, {product.Price}");
            }


            Console.WriteLine("Hello World!");
        }
    }
}
