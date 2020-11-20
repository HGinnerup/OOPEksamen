using Microsoft.VisualStudio.TestTools.UnitTesting;
using OopEksamen.Structs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace OopEksamenTest.Structs
{
    [TestClass]
    public class MoneyTest
    {
        [TestMethod]
        public void TestToString()
        {
            Money money = 450;

            var decimalSeperator = NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator;

            Assert.AreEqual($"4{decimalSeperator}50 DKK", $"{money}");
        }
        [TestMethod]
        public void TestAdd()
        {
            Money money = 450;

            money += 1000;

            Assert.AreEqual((Money)1450, money);
        }
        [TestMethod]
        public void TestSubtract()
        {
            Money money = 450;

            money -= 1000;

            Assert.AreEqual((Money)(-550), money);
        }
    }
}
