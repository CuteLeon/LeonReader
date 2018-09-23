using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeonReader.Common.Tests
{
    [TestClass()]
    public class NetUtilsTests
    {
        [TestMethod()]
        public void LinkCombineTestString()
        {
            Assert.AreEqual("https://www.cuteleon.com/welcome", NetUtils.LinkCombine("https://www.cuteleon.com", "welcome"));
            Assert.AreEqual("https://www.cuteleon.com/welcome", NetUtils.LinkCombine("https://www.cuteleon.com/", "welcome"));
            Assert.AreEqual("https://www.cuteleon.com/welcome", NetUtils.LinkCombine("https://www.cuteleon.com", "/welcome"));
            Assert.AreEqual("https://www.cuteleon.com/welcome", NetUtils.LinkCombine("https://www.cuteleon.com/", "/welcome"));
            Assert.ThrowsException<Exception>(new Action(() => NetUtils.LinkCombine("", "https://www.cuteleon.com/welcome")));
        }

        [TestMethod()]
        public void LinkCombineTestUri()
        {
            Assert.AreEqual("https://www.cuteleon.com/welcome", NetUtils.LinkCombine(new Uri("https://www.cuteleon.com"), "welcome"));
        }

        [TestMethod()]
        public void GetWebPageTestString()
        {
            Assert.IsTrue(NetUtils.GetWebPage(new Uri("http://www.cuteleon.com")).Length > 0);
        }

        [TestMethod()]
        public void GetWebPageTestUri()
        {
            Assert.IsTrue(NetUtils.GetWebPage("http://www.cuteleon.com").Length > 0);
        }

        [TestMethod()]
        public void DownloadWebFileTest()
        {
            NetUtils.DownloadWebFile("https://imgs.gamersky.com/upimg/2018/201809211810503717.jpg", @"201809211810503717.jpg");
        }
    }
}