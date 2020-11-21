using Microsoft.VisualStudio.TestTools.UnitTesting;
using OopEksamen.Classes;
using OopEksamen.Structs;
using System;
using System.Collections.Generic;
using System.Text;

namespace OopEksamenTest.Classes
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void TestCtor()
        {
            var user = new User(1, "Anders", "Hansen", "AndersHansen", "anders@hansen.dk");

            Assert.AreEqual(1u, user.ID);
            Assert.AreEqual("Anders", user.Firstname);
            Assert.AreEqual("Hansen", user.Lastname);
            Assert.AreEqual("AndersHansen", user.Username);
            Assert.AreEqual("anders@hansen.dk", user.Email);
            Assert.AreEqual((Money)0, user.Balance);
            Assert.AreEqual((Money)0, user.Credit);
        }

        [TestMethod]
        public void TestEmail()
        {
            // Valid Email
            Exception exception = null;
            try { new User(1, "Anders", "Hansen", "AndersHansen", "anders@hansen.dk"); }
            catch (Exception e) { exception = e;  }

            Assert.IsNull(exception);


            // Invalid Email
            exception = null;
            try { new User(1, "Anders", "Hansen", "AndersHansen", "anders@@hansen.dk"); }
            catch (Exception e) { exception = e; }

            Assert.IsNotNull(exception);
        }

        [TestMethod]
        public void TestToString()
        {
            var user = new User(1, "Anders", "Hansen", "AndersHansen", "anders@hansen.dk");

            Assert.AreEqual("Anders Hansen (anders@hansen.dk)", user.ToString());
        }
    }
}
