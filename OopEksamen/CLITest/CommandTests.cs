using Microsoft.VisualStudio.TestTools.UnitTesting;
using OopEksamen.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
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


            sys = Utilities.GetNewTestSystem(new string[] { ":deactivate 1", ":activate 1" });
            sys.StregSystemUI.Start();
            Assert.IsTrue(sys.StregSystem.ProductManager.GetProductByID(1).Active);
        }

        [TestMethod]
        public void TestCreditOffAndCreditOn()
        {
            StregsystemController sys;

            sys = Utilities.GetNewTestSystem(new string[] { ":crediton 1", "adods 1" });
            Assert.IsNull(sys.StregSystem.TransactionManager.GetTransactions(i => i.User.Username == "adods").FirstOrDefault());
            sys.StregSystemUI.Start();
            Assert.IsNotNull(sys.StregSystem.TransactionManager.GetTransactions(i => i.User.Username == "adods").FirstOrDefault());

            sys = Utilities.GetNewTestSystem(new string[] { ":crediton 1", ":creditoff 1", "adods 1" });
            sys.StregSystemUI.Start();
            Assert.IsNull(sys.StregSystem.TransactionManager.GetTransactions(i => i.User.Username == "adods").FirstOrDefault());


            sys = Utilities.GetNewTestSystem(new string[] { ":creditoff 1", ":crediton 1", "adods 1" });
            sys.StregSystemUI.Start();
            Assert.IsNotNull(sys.StregSystem.TransactionManager.GetTransactions(i => i.User.Username == "adods").FirstOrDefault());
        }
    }
}
