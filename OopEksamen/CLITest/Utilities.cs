using OopEksamen.Classes;
using OopEksamen.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ControllerTest
{
    public static class Utilities
    {
        public class StregsystemUITest : StregsystemUICLI
        {
            public string[] CLICommands { get; set; }
            private int _cLICommandInterator { get; set; } = 0;


            public StregsystemUITest(IStregSystem stregSystem, IEnumerable<string> cLICommands) : base(stregSystem)
            {
                CLICommands = cLICommands.ToArray();
            }

            protected override string ReadLine()
            {
                if (CLICommands.Length <= (_cLICommandInterator+1)) Close();
                var cmd = CLICommands[_cLICommandInterator];
                _cLICommandInterator++;
                return cmd;
            }
        }
        public static StregsystemController GetNewTestSystem(IEnumerable<string> cLICommands)
        {
            var testProjPath = @".\..\..\..\";

            // Refresh tempoary data
            var constDataPath = Path.Join(testProjPath, @"data_const");
            var tmpDataPath = Path.Join(testProjPath, @"data_tmp");
            var logPath = Path.Join(testProjPath, @"log");

            if(Directory.Exists(tmpDataPath)) Directory.Delete(tmpDataPath, true);
            if (Directory.Exists(logPath)) Directory.Delete(logPath, true);

            Directory.CreateDirectory(tmpDataPath);
            foreach (var filePath in Directory.GetFiles(constDataPath))
            {
                var fileName = Path.GetFileName(filePath);
                File.Copy(
                    Path.Join(constDataPath, fileName),
                    Path.Join(tmpDataPath, fileName)
                );
            }

            // Instantiate and return new controller

            IStregSystem stregSystem = new StregSystem(tmpDataPath, logPath);
            IStregsystemUI stregSystemUI = new StregsystemUITest(stregSystem, cLICommands);

            return new StregsystemController(stregSystem, stregSystemUI);
        }
    }
}
