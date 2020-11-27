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
            using IStregSystem stregSystem = new StregSystem(
                dataPath: Path.Join(visualStudioSolutionPath, @"data"),
                logDirectory: Path.Join(visualStudioSolutionPath, @"log")
                );
            using IStregsystemUI stregSystemUI = new StregsystemUICLI(stregSystem);

            new StregsystemController(stregSystem, stregSystemUI);

            stregSystemUI.Start();

        }
    }
}
