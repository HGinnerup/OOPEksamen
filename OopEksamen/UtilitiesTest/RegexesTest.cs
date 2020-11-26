using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace UtilitiesTest
{
    [TestClass]
    public class RegexesTest
    {
        [TestMethod]
        [DataRow("eksempel@domain.dk")]
        [DataRow("hginne19@student.aau.dk")]
        [DataRow("HGinne19@student.AAU.dk")]
        [DataRow("a@b.c")]
        [DataRow("a.a@b.f.c")]
        [DataRow("a.a@B.f..c")]
        [DataRow("a._A@b.f..c")]
        [DataRow("_a@b.c")]
        [DataRow("_a@b-.-c")]
        [DataRow("pizza@shrimp.burger")]
        public void EmailTestValid(string email)
        {
            Assert.IsTrue(Regexes.Email.IsMatch(email));
        }

        [TestMethod]
        [DataRow("eksempel(2)@-mit_domain.dk")]
        [DataRow("eksempel(2)@mitdomain.dk")]
        [DataRow("eksempel@-mit_domain.dk")]
        [DataRow("eksempel@mit_domain.dk")]
        [DataRow("eksempel@-mitdomain.dk")]
        [DataRow("")]
        [DataRow("@")]
        [DataRow("@.")]
        [DataRow("a@b.")]
        [DataRow("a@.c")]
        [DataRow("a@bb")]
        [DataRow("a@b.c_")]
        [DataRow("a@_b.c")]
        [DataRow("🍕@🍤.🍔")]
        public void EmailTestInvalid(string email)
        {
            Assert.IsFalse(Regexes.Email.IsMatch(email));
        }


    }
}