using Microsoft.VisualStudio.TestTools.UnitTesting;
using OopEksamen.Classes.Csv;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using OopEksamen.Models;

namespace OopEksamenTest.Classes.Csv
{
    [TestClass]
    public class CsvManagerBaseTest
    {
        const string tmpTestDirectory = "CsvUnitTestTmpData";


        [TestMethod]
        void CreateAndReadUser()
        {
            var testPath = Path.Join(tmpTestDirectory, "userTest.csv");


            var writtenUser = new User(1, "Anders", "Hansen", "andershansen", "anders@hansen.dk");

            using (var userWriter = new UserManagerCsv(testPath))
            {
                userWriter.AddUser(writtenUser);
            }

            User readUser;
            using (var userReader = new UserManagerCsv(testPath))
            {
                readUser = userReader.GetUserByUsername(writtenUser.Username);
            }

            Assert.AreEqual(writtenUser, readUser);
        }
    }
}
