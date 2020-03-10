using KeePass_HaveIBeenPwned;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Test_KP_HIBP
{
    [TestClass]
    public class UnitTest1
    {
        string Export1FileName = @"TestData\Export1.xml";


        [TestMethod]
        public void Test_ReadKeePassFile()
        {
            //string currentPath = Environment.CurrentDirectory; // D:\Source\github\Passwords\Test_KP_HIBP\bin\Debug\netcoreapp2.1
            KeePassFile keePassFile = KeePassFile.ReadFromXmlFile(Export1FileName);

            Assert.AreEqual(keePassFile.Root.Groups.Count, 1);
            Assert.AreEqual(keePassFile.Root.Groups[0].Name, "db1");
            Assert.AreEqual(keePassFile.Root.Groups[0].Groups.Count, 5);
            Assert.AreEqual(keePassFile.Root.Groups[0].Groups[0].Name, "General");
            Assert.AreEqual(keePassFile.Root.Groups[0].Groups[1].Name, "eMail");
            Assert.AreEqual(keePassFile.Root.Groups[0].Groups[3].Name, "Group B");
            Assert.AreEqual(keePassFile.Root.Groups[0].Groups[4].Name, "Recycle Bin");

            Assert.AreEqual(keePassFile.Root.Groups[0].Groups[2].Name, "Group A");
            Assert.AreEqual(keePassFile.Root.Groups[0].Groups[2].Entries[0].StringPairs[0].Key, "fooeybarrey");
            Assert.AreEqual(keePassFile.Root.Groups[0].Groups[2].Entries[0].StringPairs[0].Value.Text, "value fb");
            Assert.AreEqual(keePassFile.Root.Groups[0].Groups[2].Entries[0].StringPairs[1].Key, "Notes");
            Assert.AreEqual(keePassFile.Root.Groups[0].Groups[2].Entries[0].StringPairs[1].Value.Text, null);
            Assert.AreEqual(keePassFile.Root.Groups[0].Groups[2].Entries[0].StringPairs[2].Key, "Password");
            Assert.AreEqual(keePassFile.Root.Groups[0].Groups[2].Entries[0].StringPairs[2].Value.Text, "pass8");
            Assert.AreEqual(keePassFile.Root.Groups[0].Groups[2].Entries[0].StringPairs[5].Key, "UserName");
            Assert.AreEqual(keePassFile.Root.Groups[0].Groups[2].Entries[0].StringPairs[5].Value.Text, "foobar");

            Assert.AreEqual(keePassFile.Root.Groups[0].Groups[2].Groups.Count, 2);
            Assert.AreEqual(keePassFile.Root.Groups[0].Groups[2].Groups[0].Name, "Group AA");

        }
    }
}
