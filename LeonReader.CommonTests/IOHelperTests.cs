using Microsoft.VisualStudio.TestTools.UnitTesting;
using LeonReader.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeonReader.Common.Tests
{
    [TestClass()]
    public class IOHelperTests
    {
        [TestMethod()]
        public void PathCombineTest()
        {
            Assert.AreEqual(@"D:\dir\file.ext", IOHelper.PathCombine(@"D:\dir", "file.ext"));
            Assert.AreEqual(@"D:\dir\file.ext", IOHelper.PathCombine(@"D:\dir\", "file.ext"));

            Assert.AreEqual(@"D:\dir\file.ext", IOHelper.PathCombine(@"D:\dir\file.ext", ""));
            Assert.AreEqual(@"D:\dir\file.ext", IOHelper.PathCombine("", @"D:\dir\file.ext"));

            Assert.AreEqual(@"D:\dir\file.ext", IOHelper.PathCombine(@"D:\xxx\yyy", @"D:\dir\file.ext"));
        }

        [TestMethod()]
        public void GetFileNameTest()
        {
            Assert.AreEqual(@"file.ext", IOHelper.GetFileName(@"D:\dir\file.ext"));
        }

        [TestMethod()]
        public void GetFileNameWithoutExtensionTest()
        {
            Assert.AreEqual(@"file", IOHelper.GetFileNameWithoutExtension(@"D:\dir\file.ext"));
        }

        [TestMethod()]
        public void FileExistsTest()
        {
            Assert.IsTrue(IOHelper.FileExists(@"C:\Windows\explorer.exe"));
            Assert.IsFalse(IOHelper.FileExists(@"C:\Windows\:::explorer.exe"));
        }

        [TestMethod()]
        public void DirectoryExistsTest()
        {
            Assert.IsTrue(IOHelper.DirectoryExists(@"C:\Windows\"));
            Assert.IsFalse(IOHelper.DirectoryExists(@"C:\Windows\explorer.exe"));
        }

        [TestMethod()]
        public void ReadeImageByStreamTest()
        {
            Assert.IsNotNull(IOHelper.ReadeImageByStream("Leon_Mathilda.jpg"));
            Assert.IsNull(IOHelper.ReadeImageByStream("FileNotExists.jpg"));
        }
    }
}