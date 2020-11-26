using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;

namespace UtilitiesTest
{
    [TestClass]
    public class StringHandlingTest
    {
        [TestMethod]
        [DataRow(@"""asd"",""as,dc""", ',', new[] { "asd", "as,dc" })]
        [DataRow(@"asd,asdc", ',', new[] { "asd", "asdc" })]
        [DataRow(@"asd,""asd""", ',', new[] { "asd", "asd" })]
        [DataRow(@"asd,""\\asd""", ',', new[] { "asd", "\\asd" })]
        [DataRow(@"asd,\""\\asd""", ',', new[] { "asd", "\"\\asd\"" })]
        [DataRow(@"asd,asd""", ',', new[] { "asd","asd\"" })]
        public void SplitStringTest(string str, char delimiter, string[] expected)
        {
            var computed = StringHandling.SplitString(str, delimiter).ToArray();
            Assert.AreEqual(expected.Length, computed.Length);
            for(var i=0; i<expected.Length; i++)
            {
                Assert.AreEqual(expected[i], computed[i]);
            }

        }
    }
}
