using Microsoft.VisualStudio.TestTools.UnitTesting;
using LeonReader.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeonReader.DataAccess;

namespace LeonReader.DataAccess.Tests
{
    [TestClass()]
    public class UnityDBContextTests
    {

        [TestMethod()]
        public void UnityDBContextTest()
        {
            UnityDBContext context = new UnityDBContext();
            context.Articles.RemoveRange(context.Articles.ToArray());
            context.Contents.RemoveRange(context.Contents.ToArray());
            context.SaveChanges();

            //测试文章表
            int startCount = context.Articles.Count();
            Console.WriteLine($"初始文章数目：{startCount}");

            Article article = new Article()
            {
                ArticleID = "100001",
                Title = "测试新增文章",
                ArticleLink = "http://www.cuteleon.com",
                Description = "测试新增文章",
                PublishTime = DateTime.Now.ToString(),
                SADESource = "DBContextTest",
                DownloadDirectoryName = "测试缓存目录",
                ArticleFileName = "测试缓存文件名称",
                State = Article.ArticleStates.New,
                Contents = new ContentItem[] {
                        new ContentItem(){ ImageDescription = "测试新增文章" },
                        new ContentItem(){ ImageDescription = "欢迎使用 Leon Reader."},
                        new ContentItem(){ ImageDescription = "Best Wishes !"}
                    }.ToList(),
            };

            context.Articles.Add(article);
            context.SaveChanges();

            int endCount = context.Articles.Count();
            Console.WriteLine($"增加文章后数目：{endCount}");
            Assert.AreEqual(startCount + 1, endCount);

            //测试内容表
            startCount = context.Articles.First(
                art => art.ArticleID == article.ArticleID && art.SADESource == article.SADESource
                ).Contents.Count;
            Console.WriteLine($"数据库内文章初始关联内容数目：{startCount}");

            article.Contents.Add(new ContentItem("测试插入内容-1"));
            article.Contents.Add(new ContentItem("测试插入内容-2"));
            article.Contents.Add(new ContentItem("测试插入内容-3"));
            context.SaveChanges();

            Console.WriteLine("增加文章内容后...");
            Console.WriteLine($"文章对象关联内容数目：{article.Contents.Count}");
            endCount = context.Articles.First(
                art => art.ArticleID == article.ArticleID && art.SADESource == article.SADESource
                ).Contents.Count;
            Console.WriteLine($"数据库内文章关联内容数目：{endCount}");
            Assert.AreEqual(startCount + 3, article.Contents.Count);
            Assert.AreEqual(endCount, article.Contents.Count);
        }

        [TestMethod]
        public void TestSaveChnages()
        {
            UnityDBContext context = new UnityDBContext();
            Article article = context.Articles.First();
            Console.WriteLine($"文章 {article.Title}({article.ArticleID}) 初始内容数：{article.Contents.Count}");
            Console.WriteLine($"数据库内容表查询：{context.Contents.Count()}");

            article.Contents.Add(new ContentItem("test add."));
            context.SaveChanges();

            Console.WriteLine($"数据库内容表查询：{context.Contents.Count()}");
        }

    }
}