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
    public class NetHelperTests
    {
        [TestMethod()]
        public void LinkCombineTestString()
        {
            Assert.AreEqual("https://www.cuteleon.com/welcome", NetHelper.LinkCombine("https://www.cuteleon.com", "welcome"));
            Assert.AreEqual("https://www.cuteleon.com/welcome", NetHelper.LinkCombine("https://www.cuteleon.com/", "welcome"));
            Assert.AreEqual("https://www.cuteleon.com/welcome", NetHelper.LinkCombine("https://www.cuteleon.com", "/welcome"));
            Assert.AreEqual("https://www.cuteleon.com/welcome", NetHelper.LinkCombine("https://www.cuteleon.com/", "/welcome"));
            Assert.ThrowsException<Exception>(new Action(() => NetHelper.LinkCombine("", "https://www.cuteleon.com/welcome")));
        }

        [TestMethod()]
        public void LinkCombineTestUri()
        {
            Assert.AreEqual("https://www.cuteleon.com/welcome", NetHelper.LinkCombine(new Uri("https://www.cuteleon.com"), "welcome"));
        }

        [TestMethod()]
        public void GetWebPageTestString()
        {
            Assert.IsTrue(NetHelper.GetWebPage(new Uri("http://www.cuteleon.com")).Length > 0);
        }

        [TestMethod()]
        public void GetWebPageTestUri()
        {
            Assert.IsTrue(NetHelper.GetWebPage("http://www.cuteleon.com").Length > 0);
        }

        [TestMethod()]
        public void DownloadWebFileTest()
        {
            NetHelper.DownloadWebFile("https://imgs.gamersky.com/upimg/2018/201809211810503717.jpg", @"201809211810503717.jpg");
        }
    }
}