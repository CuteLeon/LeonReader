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
    }
}