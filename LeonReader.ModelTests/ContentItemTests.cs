using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            Assert.AreEqual("内容-0", content_0.ImageDescription);

            ContentItem content_1 = new ContentItem("内容-1");
            Assert.AreEqual("内容-1", content_1.ImageDescription);

            ContentItem content_2 = new ContentItem("内容-2", "http://www.cuteleon.com/contentimage-2.jpg");
            Assert.AreEqual("内容-2", content_2.ImageDescription);
            Assert.AreEqual("http://www.cuteleon.com/contentimage-2.jpg", content_2.ImageLink);

            ContentItem content_3 = new ContentItem("内容-3", "http://www.cuteleon.com/contentimage-3.jpg", "内容-3.jpg");
            Assert.AreEqual("内容-3", content_3.ImageDescription);
            Assert.AreEqual("http://www.cuteleon.com/contentimage-3.jpg", content_3.ImageLink);
            Assert.AreEqual("内容-3.jpg", content_3.ImageFileName);
        }
    }
}