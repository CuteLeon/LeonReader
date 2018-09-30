using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

using LeonReader.AbstractSADE;
using LeonReader.Common;
using LeonReader.Model;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GamerSkySADE.Tests
{
    [TestClass()]
    public class GamerSkyScannerTests
    {
        [TestMethod()]
        public void ProcessTest()
        {
            Scanner scanner = new GamerSkyScanner();

            //用于仅测试进度报告功能
            LogUtils.LogLevel = LogUtils.LogTypes.FATAL;
            scanner.ProcessReport += (s, e) => { LogUtils.Fatal($"扫描进度：{e.ProgressPercentage} 篇文章"); };

            scanner.Process();

            //睡眠一段时间，否则调试线程不会等待异步任务线程而立即结束
            Thread.Sleep(5000);
        }

        [TestMethod()]
        public void ScanArticlesTest()
        {
            GamerSkyScanner scanner = new GamerSkyScanner();
            MethodInfo methodInfo = typeof(GamerSkyScanner).GetMethod(
                "ScanArticles",
                BindingFlags.NonPublic | BindingFlags.Instance
            );
            if (methodInfo == null)
            {
                Console.WriteLine("反射获取方法失败");
                Assert.Fail();
            }

            //在线资源测试
            foreach (var article in
                (IEnumerable<Article>)methodInfo.Invoke(
                    scanner,
                    new object[] { NetUtils.GetWebPage(scanner.TargetCatalogURI) }
                )
            )
            {
                Console.WriteLine($"扫描到文章：{article.Title} ({article.ArticleID})");
            }

            //离线资源测试
            foreach (var article in
                (IEnumerable<Article>)methodInfo.Invoke(
                    scanner,
                    new object[] { GamerSkySADETests.UnitTestResource.FullTestResource }
                )
            )
            {
                Console.WriteLine($"扫描到文章：{article.Title} ({article.ArticleID})");
            }
        }

        [TestMethod()]
        public void ConvertToArticleTest()
        {
            GamerSkyScanner scanner = new GamerSkyScanner();
            MethodInfo methodInfo = typeof(GamerSkyScanner).GetMethod(
                "ConvertToArticle",
                BindingFlags.NonPublic | BindingFlags.Instance
            );
            if (methodInfo == null)
            {
                Console.WriteLine("反射获取方法失败");
                Assert.Fail();
            }

            Article article = (Article)methodInfo.Invoke(scanner, new object[] { GamerSkySADETests.UnitTestResource.ConvertToArticleTestResource });
            
            Assert.IsNotNull(article);
            Assert.AreEqual("文章链接", article.ArticleLink);
            Assert.AreEqual("文章描述", article.Description);
            Assert.AreEqual("图像链接", article.ImageLink);
            Assert.AreEqual("发布时间", article.PublishTime);
            Assert.AreEqual("文章标题", article.Title);
        }

        [TestMethod()]
        public void GetCatalogListTest()
        {
            GamerSkyScanner scanner = new GamerSkyScanner();
            MethodInfo methodInfo = typeof(GamerSkyScanner).GetMethod(
                "GetCatalogList",
                BindingFlags.NonPublic | BindingFlags.Instance
            );
            if (methodInfo == null)
            {
                Console.WriteLine("反射获取方法失败");
                Assert.Fail();
            }

            string[] catalogList = (string[])methodInfo.Invoke(scanner, new object[] { GamerSkySADETests.UnitTestResource.GetCatalogListTestResource });
            Assert.IsTrue(catalogList.Length > 0);
        }

        [TestMethod()]
        public void GetCatalogContentTest()
        {
            GamerSkyScanner scanner = new GamerSkyScanner();
            MethodInfo methodInfo = typeof(GamerSkyScanner).GetMethod(
                "GetCatalogContent",
                BindingFlags.NonPublic | BindingFlags.Instance
            );
            if (methodInfo == null)
            {
                Console.WriteLine("反射获取方法失败");
                Assert.Fail();
            }
            
            //离线资源测试
            string catalogListOffLine = (string)methodInfo.Invoke(scanner, new object[] { GamerSkySADETests.UnitTestResource.FullTestResource });
            Assert.IsTrue(catalogListOffLine.Length > 0);

            //在线资源测试
            string catalogListOnLine = (string)methodInfo.Invoke(scanner, new object[] { NetUtils.GetWebPage(scanner.TargetCatalogURI) });
            Assert.IsTrue(catalogListOnLine.Length > 0);
        }
    }
}