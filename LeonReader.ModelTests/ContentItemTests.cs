using Microsoft.VisualStudio.TestTools.UnitTesting;
using LeonReader.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeonReader.Model.Tests
{
    [TestClass()]
    public class ContentItemTests
    {
        [TestMethod()]
        public void ContentItemTest()
        {
            ContentItem content_0 = new ContentItem();
            content_0.ImageDescription = "内容-0";
            Assert.AreEqual(content_0.ImageDescription,"内容-0");

            ContentItem content_1 = new ContentItem("内容-1");
            Assert.AreEqual(content_1.ImageDescription,"内容-1");

            ContentItem content_2 = new ContentItem("内容-2", "http://www.cuteleon.com/contentimage-2.jpg");
            Assert.AreEqual(content_2.ImageDescription, "内容-2");
            Assert.AreEqual(content_2.ImageLink, "http://www.cuteleon.com/contentimage-2.jpg");

            ContentItem content_3 = new ContentItem("内容-3", "http://www.cuteleon.com/contentimage-3.jpg", "内容-3.jpg");
            Assert.AreEqual(content_3.ImageDescription, "内容-3");
            Assert.AreEqual(content_3.ImageLink, "http://www.cuteleon.com/contentimage-3.jpg");
            Assert.AreEqual(content_3.ImageFileName, "内容-3.jpg");
        }
    }
}