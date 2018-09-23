using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeonReader.Common.Tests
{
    [TestClass()]
    public class IOUtilsTests
    {
        [TestMethod()]
        public void PathCombineTest()
        {
            Assert.AreEqual(@"D:\dir\file.ext", IOUtils.PathCombine(@"D:\dir", "file.ext"));
            Assert.AreEqual(@"D:\dir\file.ext", IOUtils.PathCombine(@"D:\dir\", "file.ext"));

            Assert.AreEqual(@"D:\dir\file.ext", IOUtils.PathCombine(@"D:\dir\file.ext", ""));
            Assert.AreEqual(@"D:\dir\file.ext", IOUtils.PathCombine("", @"D:\dir\file.ext"));

            Assert.AreEqual(@"D:\dir\file.ext", IOUtils.PathCombine(@"D:\xxx\yyy", @"D:\dir\file.ext"));
        }

        [TestMethod()]
        public void GetFileNameTest()
        {
            Assert.AreEqual(@"file.ext", IOUtils.GetFileName(@"D:\dir\file.ext"));
        }

        [TestMethod()]
        public void GetFileNameWithoutExtensionTest()
        {
            Assert.AreEqual(@"file", IOUtils.GetFileNameWithoutExtension(@"D:\dir\file.ext"));
        }

        [TestMethod()]
        public void FileExistsTest()
        {
            Assert.IsTrue(IOUtils.FileExists(@"C:\Windows\explorer.exe"));
            Assert.IsFalse(IOUtils.FileExists(@"C:\Windows\:::explorer.exe"));
        }

        [TestMethod()]
        public void DirectoryExistsTest()
        {
            Assert.IsTrue(IOUtils.DirectoryExists(@"C:\Windows\"));
            Assert.IsFalse(IOUtils.DirectoryExists(@"C:\Windows\explorer.exe"));
        }

        [TestMethod()]
        public void ReadeImageByStreamTest()
        {
            Assert.IsNotNull(IOUtils.ReadeImageByStream("Leon_Mathilda.jpg"));
            Assert.IsNull(IOUtils.ReadeImageByStream("FileNotExists.jpg"));
        }

        [TestMethod()]
        public void GetSafeFileNameTest()
        {
            string TestName = "ABC:DEF>GHI\\";
            Assert.AreEqual("ABCDEFGHI", IOUtils.GetSafeFileName(TestName));
        }

        [TestMethod()]
        public void GetSafeDirectoryNameTest()
        {
            string TestName = "ABC:DEF>GHI\\";
            Assert.AreEqual("ABCDEFGHI", IOUtils.GetSafeFileName(TestName));
        }
    }
}