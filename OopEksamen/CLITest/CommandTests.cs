using Microsoft.VisualStudio.TestTools.UnitTesting;
using OopEksamen.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControllerTest
{
    [TestClass]
    public class CommandTests
    {
        [TestMethod]
        public void TestActivateAndDeActivate()
        {
            StregsystemController sys;

            sys = Utilities.GetNewTestSystem(new string[] { ":activate 1" });
            sys.StregSystemUI.Start();
            Assert.IsTrue(sys.StregSystem.ProductManager.GetProductByID(1).Active);

            sys = Utilities.GetNewTestSystem(new string[] { ":activate 1", ":deactivate 1" });
            sys.StregSystemUI.Start();
            Assert.IsFalse(sys.StregSystem.ProductManager.GetProductByID(1).Active);


            sys = Utilities.GetNewTestSystem(new string[] { ":activate 1", ":deactivate 1", ":activate 1" });
            sys.StregSystemUI.Start();
            Assert.IsTrue(sys.StregSystem.ProductManager.GetProductByID(1).Active);
        }
    }
}
