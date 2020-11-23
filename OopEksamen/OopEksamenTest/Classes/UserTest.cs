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
        private User NewValidUser()
        {
            return new User(1, "Anders", "Hansen", "AndersHansen", "anders@hansen.dk");
        }

        [TestMethod]
        public void TestCtor()
        {
            var user = NewValidUser();

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
            var user = new User(1, "Anders", "Hansen", "AndersHansen", "anders@hansen.dk");
            Assert.AreEqual("anders@hansen.dk", user.Email);
            user.Email = "ole@jensen.dk";
            Assert.AreEqual("ole@jensen.dk", user.Email);

            // Invalid Email
            Assert.ThrowsException<Exception>(() => new User(1, "Anders", "Hansen", "AndersHansen", "anders@@hansen.dk"));
            Assert.ThrowsException<Exception>(() => new User(1, "Anders", "Hansen", "AndersHansen", ""));
            Assert.ThrowsException<Exception>(() => new User(1, "Anders", "Hansen", "AndersHansen", null));

            Assert.ThrowsException<Exception>(() => NewValidUser().Email = "anders@@hansen.dk");
            Assert.ThrowsException<Exception>(() => NewValidUser().Email = "");
            Assert.ThrowsException<Exception>(() => NewValidUser().Email = null);
        }

        [TestMethod]
        public void TestToString()
        {
            var user = new User(1, "Anders", "Hansen", "AndersHansen", "anders@hansen.dk");

            Assert.AreEqual("Anders Hansen (anders@hansen.dk)", user.ToString());
        }


        /// <summary>
        /// Used to test whether User.Equals requires a type-match
        /// </summary>
        private class FakeUserClass { 
            public uint ID { get; set; } 
        };
        [TestMethod]
        public void TestEqual()
        {
            var user1 = NewValidUser();
            var user2 = NewValidUser();

            Assert.IsNotNull(user1);

            Assert.IsTrue(user1.Equals(user2));

            Assert.IsFalse(user1.Equals(null));

            // Require type
            Assert.IsFalse(user1.Equals(new DateTime()));
            Assert.IsFalse(user1.Equals(new FakeUserClass() { ID = user1.ID })); ;

            var differentUser = new User(2, "Anders", "Hansen", "AndersHansen", "anders@hansen.dk");
            Assert.IsFalse(user1.Equals(differentUser));

        }
    }
}
