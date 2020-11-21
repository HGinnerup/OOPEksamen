using Microsoft.VisualStudio.TestTools.UnitTesting;
using OopEksamen.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OopEksamenTest.Utilities
{
    [TestClass]
    public class RegexesTest
    {
        [TestMethod]
        public void EmailTest()
        {
            Assert.IsTrue(Regexes.Email.IsMatch("eksempel@domain.dk"));
            Assert.IsTrue(Regexes.Email.IsMatch("hginne19@student.aau.dk"));
            Assert.IsTrue(Regexes.Email.IsMatch("HGinne19@student.AAU.dk"));
            Assert.IsTrue(Regexes.Email.IsMatch("a@b.c"));
            Assert.IsTrue(Regexes.Email.IsMatch("a.a@b.f.c"));
            Assert.IsTrue(Regexes.Email.IsMatch("a.a@B.f..c"));
            Assert.IsTrue(Regexes.Email.IsMatch("a._A@b.f..c"));
            Assert.IsTrue(Regexes.Email.IsMatch("_a@b.c"));
            Assert.IsTrue(Regexes.Email.IsMatch("_a@b-.-c"));
            Assert.IsTrue(Regexes.Email.IsMatch("pizza@shrimp.burger"));

            Assert.IsFalse(Regexes.Email.IsMatch("eksempel(2)@-mit_domain.dk"));
            Assert.IsFalse(Regexes.Email.IsMatch("eksempel(2)@mitdomain.dk"));
            Assert.IsFalse(Regexes.Email.IsMatch("eksempel@-mit_domain.dk"));
            Assert.IsFalse(Regexes.Email.IsMatch("eksempel@mit_domain.dk"));
            Assert.IsFalse(Regexes.Email.IsMatch("eksempel@-mitdomain.dk"));
            Assert.IsFalse(Regexes.Email.IsMatch("a@b."));
            Assert.IsFalse(Regexes.Email.IsMatch("a@.c"));
            Assert.IsFalse(Regexes.Email.IsMatch("a@bb"));
            Assert.IsFalse(Regexes.Email.IsMatch("a@b.c_"));
            Assert.IsFalse(Regexes.Email.IsMatch("a@_b.c"));
            Assert.IsFalse(Regexes.Email.IsMatch("🍕@🍤.🍔"));
        }
    }
}