using Microsoft.VisualStudio.TestTools.UnitTesting;
using GamerSkySADE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeonReader.AbstractSADE;
using LeonReader.Model;
using System.Reflection;
using LeonReader.Common;
using System.Threading;

namespace GamerSkySADE.Tests
{
    [TestClass()]
    public class GamerSkyAnalyzerTests
    {
        [TestMethod()]
        public void ProcessTest()
        {
            string link = @"https://www.gamersky.com/ent/201808/1094495.shtml";
            Analyzer analyzer = new GamerSkyAnalyzer();
            
            //用于仅测试进度报告功能
            LogUtils.LogLevel = LogUtils.LogTypes.FATAL;
            analyzer.ProcessReport += (s, e) => { LogUtils.Fatal($"分析进度：{e.ProgressPercentage} 页，{(int)e.UserState}图"); };
            Article article = new Article()
            {
                ArticleID = "10001",
                Title = "单元测试文章_1",
                ArticleLink = link,
                Description = "单元测试文章_1",
                IsNew = true,
                PublishTime = DateTime.Now.ToString(),
                SADESource = analyzer.SADESource,
                ScanTime = DateTime.Now,
                Contents = new ContentItem[] {
                        new ContentItem("单元测试文章_1"),
                        new ContentItem("欢迎使用 Leon Reader."),
                        new ContentItem("Best Wishes !")
                    }.ToList(),
                DownloadDirectoryName = "单元测试下载目录",
                ArticleFileName = "单元测试文章名称",
            };
            analyzer.TargetArticleManager.RemoveArticles(analyzer.TargetArticleManager.GetArticles(analyzer.SADESource).ToArray());

            analyzer.Process(article);
            
            //睡眠一段时间，否则调试线程不会等待异步任务线程而立即结束
            Thread.Sleep(10000);
        }
        
        [TestMethod()]
        public void AnalyseArticleTest()
        {
            Analyzer analyzer = new GamerSkyAnalyzer();
            MethodInfo methodInfo = typeof(GamerSkyAnalyzer).GetMethod(
                "AnalyseArticle",
                BindingFlags.NonPublic | BindingFlags.Instance
            );
            if (methodInfo == null)
            {
                Console.WriteLine("反射获取方法失败");
                Assert.Fail();
            }

            string link = @"https://www.gamersky.com/ent/201808/1094495.shtml";
            methodInfo.Invoke(analyzer, new object[] { link });
        }

        [TestMethod()]
        public void GetNextLinkTest()
        {
            Analyzer analyzer = new GamerSkyAnalyzer();
            MethodInfo methodInfo = typeof(GamerSkyAnalyzer).GetMethod(
                "GetNextLink",
                BindingFlags.NonPublic | BindingFlags.Instance
            );
            if (methodInfo == null)
            {
                Console.WriteLine("反射获取方法失败");
                Assert.Fail();
            }

            Assert.AreSame(string.Empty, methodInfo.Invoke(analyzer, new object[] { GamerSkySADETests.UnitTestResource.PaginationLast }));
            Assert.AreNotSame(string.Empty, methodInfo.Invoke(analyzer, new object[] { GamerSkySADETests.UnitTestResource.PaginationCommon }));
        }

        [TestMethod()]
        public void GetArticleContentTest()
        {
            Analyzer analyzer = new GamerSkyAnalyzer();
            MethodInfo methodInfo = typeof(GamerSkyAnalyzer).GetMethod(
                "GetArticleContent",
                BindingFlags.NonPublic | BindingFlags.Instance
            );
            if (methodInfo == null)
            {
                Console.WriteLine("反射获取方法失败");
                Assert.Fail();
            }

            Assert.AreNotSame(string.Empty, methodInfo.Invoke(analyzer, new object[] { GamerSkySADETests.UnitTestResource.FullPageContent }));
        }

        [TestMethod()]
        public void SplitContentAndPaginationTest()
        {
            Analyzer analyzer = new GamerSkyAnalyzer();
            MethodInfo methodInfo = typeof(GamerSkyAnalyzer).GetMethod(
                "SplitContentAndPagination",
                BindingFlags.NonPublic | BindingFlags.Instance
            );
            if (methodInfo == null)
            {
                Console.WriteLine("反射获取方法失败");
                Assert.Fail();
            }

            Tuple<string, string> tuple = methodInfo.Invoke(analyzer, new object[] { GamerSkySADETests.UnitTestResource.FullPageContent }) as Tuple<string, string>;
            Assert.IsNotNull(tuple);
            Assert.IsNotNull(tuple.Item1);
            Assert.IsNotNull(tuple.Item2);
        }

        [TestMethod()]
        public void ConvertToContentItemTest()
        {
            Analyzer analyzer = new GamerSkyAnalyzer();
            MethodInfo methodInfo = typeof(GamerSkyAnalyzer).GetMethod(
                "ConvertToContentItem",
                BindingFlags.NonPublic | BindingFlags.Instance
            );
            if (methodInfo == null)
            {
                Console.WriteLine("反射获取方法失败");
                Assert.Fail();
            }

            Assert.IsNotNull(methodInfo.Invoke(analyzer, new object[] { GamerSkySADETests.UnitTestResource.Content_1 }));
            Assert.IsNotNull(methodInfo.Invoke(analyzer, new object[] { GamerSkySADETests.UnitTestResource.Content_2 }));
        }

        [TestMethod()]
        public void GetContentListTest()
        {
            Analyzer analyzer = new GamerSkyAnalyzer();
            MethodInfo methodInfo = typeof(GamerSkyAnalyzer).GetMethod(
                "GetContentList",
                BindingFlags.NonPublic | BindingFlags.Instance
            );
            if (methodInfo == null)
            {
                Console.WriteLine("反射获取方法失败");
                Assert.Fail();
            }

            Assert.IsTrue((methodInfo.Invoke(analyzer, new object[] { GamerSkySADETests.UnitTestResource.MainContent }) as string[]).Length > 0);
        }
    }
}