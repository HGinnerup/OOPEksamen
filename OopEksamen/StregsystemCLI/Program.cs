using System;
using StregsystemCLI.Classes;
using StregsystemCLI.Interfaces;

namespace StregsystemCLI
{
    class Program
    {
        static void EchoCommand(string rawString, string command, string[] args)
        {
            Console.WriteLine("RawString: " + rawString);
            Console.WriteLine("Command: " + command);
            Console.WriteLine("Args: ");
            foreach(var arg in args)
            {
                Console.WriteLine("\t"+arg);
            }
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            var stregSystemCli = new StregsystemUICLI();
            stregSystemCli.CommandEntered += EchoCommand;
            stregSystemCli.Start();
            Console.WriteLine("Hello World!");
        }
    }
}
