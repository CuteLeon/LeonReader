using System;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeonReader.Model.Tests
{
    [TestClass()]
    public class ArticleTests
    {
        [TestMethod()]
        public void ArticleTest()
        {
            Article article = 
                new Article()
                {
                    ArticleID = "10000",
                    Title = "单元测试文章",
                    ArticleLink = "http://www.cuteleon.com",
                    Description = "单元测试文章",
                    PublishTime = DateTime.Now.ToString(),
                    SADESource = "DataSeed",
                    Contents = new ContentItem[] {
                        new ContentItem(){ ImageDescription = "种子文章" },
                        new ContentItem(){ ImageDescription = "欢迎使用 Leon Reader."},
                        new ContentItem(){ ImageDescription = "Best Wishes !"}
                    }.ToList(),
                };

            Assert.AreEqual(3, article.Contents.Count);
            Assert.AreEqual("种子文章", article.Contents.FirstOrDefault().ImageDescription);
        }
    }
}