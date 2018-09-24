using System.Drawing;

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
            Image image = IOUtils.ReadeImage("Leon_Mathilda.jpg");
            Assert.IsNotNull(image);
            using (Graphics graphics = Graphics.FromImage(image))
            {
                graphics.FillEllipse(Brushes.Red, 10, 10, image.Width - 20, image.Height - 20);
            }
            image.Save("New.jpg");

            Assert.ThrowsException<System.IO.FileNotFoundException>(
                () => IOUtils.ReadeImage("FileNotExists.jpg"));
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