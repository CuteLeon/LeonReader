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
    public class ArticleTests
    {
        [TestMethod()]
        public void ArticleTest()
        {
            Article article = 
                new Article(
                    "10000",
                    "单元测试文章",
                    "http://www.cuteleon.com"
                )
                {
                    Description = "单元测试文章",
                    IsNew = true,
                    PublishTime = DateTime.Now.ToString(),
                    Contents = new ContentItem[] {
                        new ContentItem(){ ImageDescription = "种子文章" },
                        new ContentItem(){ ImageDescription = "欢迎使用 Leon Reader."},
                        new ContentItem(){ ImageDescription = "Best Wishes !"}
                    }.ToList(),
                };

            Assert.AreEqual(article.DownloadDirectory,$"{Environment.CurrentDirectory}\\10000");
            Assert.AreEqual(article.Contents.Count,3);
            Assert.AreEqual(article.ArticleFilePath,  $"{Environment.CurrentDirectory}\\{10000}\\单元测试文章");
            Assert.AreEqual(article.Contents.FirstOrDefault().ImageDescription, "种子文章");
        }
    }
}