using Microsoft.VisualStudio.TestTools.UnitTesting;
using OopEksamen.Models;
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
        [DataRow("anders@hansen.dk")]
        [DataRow("ole@jensen.dk")]
        [DataRow("hginne19@student.aau.dk")]
        [DataRow("HGinne19@student.AAU.dk")]
        [DataRow(null)]
        public void TestValidEmail(string email)
        {
            var user1 = new User(1, "Anders", "Hansen", "AndersHansen", email);
            Assert.AreEqual(email, user1.Email);

            var user2 = new User(1, "Anders", "Hansen", "AndersHansen", "a@b.c");
            user2.Email = email;
            Assert.AreEqual(email, user2.Email);
        }

        [TestMethod]
        [DataRow("anders@@hansen.dk")]
        [DataRow("")]
        public void TestInvalidEmail(string email)
        {
            Assert.ThrowsException<ArgumentException>(() => new User(1, "Anders", "Hansen", "AndersHansen", email));
            Assert.ThrowsException<ArgumentException>(() => NewValidUser().Email = email);
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
