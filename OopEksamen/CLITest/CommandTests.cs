using Microsoft.VisualStudio.TestTools.UnitTesting;
using OopEksamen.Classes;
using OopEksamen.Structs;
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

        [TestMethod]
        public void TestAddCredits()
        {
            StregsystemController sys;

            sys = Utilities.GetNewTestSystem(new string[] { });
            Assert.AreEqual(0, sys.StregSystem.GetUserByUsername("adods").Balance);

            sys = Utilities.GetNewTestSystem(new string[] { ":addcredits adods 100" });
            sys.StregSystemUI.Start();
            Assert.AreEqual(100, sys.StregSystem.GetUserByUsername("adods").Balance);


            sys = Utilities.GetNewTestSystem(new string[] { ":addcredits adods 4,5", ":addcredits adods 3.2" });
            sys.StregSystemUI.Start();
            Assert.AreEqual((Money)(decimal)7.7, sys.StregSystem.GetUserByUsername("adods").Balance);

            sys = Utilities.GetNewTestSystem(new string[] { ":addcredits adods 10", ":addcredits adods -7" });
            sys.StregSystemUI.Start();
            Assert.AreEqual(3, sys.StregSystem.GetUserByUsername("adods").Balance);


        }


    }
}
