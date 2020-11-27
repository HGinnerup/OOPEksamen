using Microsoft.VisualStudio.TestTools.UnitTesting;
using OopEksamen.Classes;
using OopEksamen.Interfaces;
using System;
using System.IO;
using System.Text;

namespace OopEksamenTest.Classes
{
    public class CliCommandTest
    {
        public string RawString { get; set; } = string.Empty;
        public string Command { get; set; } = string.Empty;
        public string[] Args { get; set; } = null;

        public void TestCommand(string rawString, string command, string[] args)
        {
            RawString = rawString;
            Command = command;
            Args = args;
        }
    }

    internal class _StregsystemUICLI : StregsystemUICLI
    {
        public string ReadLineReturnVal { get; set; }
        protected override string ReadLine()
        {
            Dispose();
            return ReadLineReturnVal;
        }
    }

    [TestClass]
    public class StregsystemCLIUITest
    {
        [TestMethod]
        [DataRow("CommandTest", "CommandTest", new string[] { })]
        [DataRow("CommandTest Arg1", "CommandTest", new string[] { "Arg1" })]
        [DataRow("CommandTest Arg1 Arg2", "CommandTest", new string[] { "Arg1", "Arg2" })]
        [DataRow("CommandTest Arg1 \"Arg2 Arg2\"", "CommandTest", new string[] { "Arg1", "Arg2 Arg2" })]
        [DataRow(":CommandTest Arg1 \"Arg2 Arg2\"", ":CommandTest", new string[] { "Arg1", "Arg2 Arg2" })]
        [DataRow(":CommandTest Arg1 \"Arg2 A\\\" rg2\"", ":CommandTest", new string[] { "Arg1", "Arg2 A\" rg2" })]
        public void TestCLICommand(string raw, string cmd, string[] args)
        {
            var stregsystemUICLI = new _StregsystemUICLI();
            var cliCommandTest = new CliCommandTest();

            stregsystemUICLI.CommandEntered += cliCommandTest.TestCommand;
            stregsystemUICLI.ReadLineReturnVal = raw;
            stregsystemUICLI.Start();

            Assert.AreEqual(raw, cliCommandTest.RawString);
            Assert.AreEqual(cmd, cliCommandTest.Command);
            Assert.AreEqual(args.Length, cliCommandTest.Args.Length);
            for (var i = 0; i < args.Length; i++)
            {
                Assert.AreEqual(args[i], cliCommandTest.Args[i]);
            }
        }
    }
}
