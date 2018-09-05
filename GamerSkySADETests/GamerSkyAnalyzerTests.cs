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
            analyzer.SetTargetURI(@link);

            analyzer.TargetDBContext.Articles.RemoveRange(analyzer.TargetDBContext.Articles.ToArray());
            analyzer.TargetDBContext.Articles.Add(
                new Article()
                {
                    ArticleID = "10001",
                    Title = "单元测试文章_1",
                    ArticleLink = link,
                    Description = "单元测试文章_1",
                    IsNew = true,
                    PublishTime = DateTime.Now.ToString(),
                    ASDESource = analyzer.ASDESource,
                    ScanTime = DateTime.Now,
                    Contents = new ContentItem[] {
                        new ContentItem("单元测试文章_1"),
                        new ContentItem("欢迎使用 Leon Reader."),
                        new ContentItem("Best Wishes !")
                    }.ToList(),
                }
            );
            analyzer.TargetDBContext.SaveChanges();

            analyzer.Process();
        }

        [TestMethod()]
        public void GetArticleTest()
        {
            Analyzer analyzer = new GamerSkyAnalyzer();
            MethodInfo methodInfo = typeof(GamerSkyAnalyzer).GetMethod(
                "GetArticle",
                BindingFlags.NonPublic | BindingFlags.Instance
            );
            if (methodInfo == null)
            {
                Console.WriteLine("反射获取方法失败");
                Assert.Fail();
            }

            analyzer.TargetDBContext.Articles.RemoveRange(analyzer.TargetDBContext.Articles.ToArray());
            analyzer.TargetDBContext.SaveChanges();

            Assert.IsNull(methodInfo.Invoke(analyzer, new object[] { "@link", analyzer.ASDESource }));

            analyzer.TargetDBContext.Articles.Add(
                new Article()
                {
                    ArticleID = "10002",
                    Title = "单元测试文章_2",
                    ArticleLink = "@link",
                    Description = "单元测试文章_2",
                    IsNew = true,
                    PublishTime = DateTime.Now.ToString(),
                    ASDESource = analyzer.ASDESource,
                    ScanTime = DateTime.Now,
                    Contents = new ContentItem[] {
                        new ContentItem("单元测试文章_2"),
                        new ContentItem("欢迎使用 Leon Reader."),
                        new ContentItem("Best Wishes !")
                    }.ToList(),
                }
            );
            analyzer.TargetDBContext.SaveChanges();

            Assert.IsNotNull(methodInfo.Invoke(analyzer, new object[] { "@link", analyzer.ASDESource }));
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
            analyzer.SetTargetURI(link);
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

            string link = @"https://www.gamersky.com/ent/201808/1094495.shtml";
            analyzer.SetTargetURI(link);

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

            string link = @"https://www.gamersky.com/ent/201808/1094495.shtml";
            analyzer.SetTargetURI(link);

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

            string link = @"https://www.gamersky.com/ent/201808/1094495.shtml";
            analyzer.SetTargetURI(link);
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

            string link = @"https://www.gamersky.com/ent/201808/1094495.shtml";
            analyzer.SetTargetURI(link);

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

            string link = @"https://www.gamersky.com/ent/201808/1094495.shtml";
            analyzer.SetTargetURI(link);

            Assert.IsTrue((methodInfo.Invoke(analyzer, new object[] { GamerSkySADETests.UnitTestResource.MainContent }) as string[]).Length > 0);
        }
    }
}