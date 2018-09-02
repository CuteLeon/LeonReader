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
    public class UnityDBContextTests
    {
        [TestMethod()]
        public void UnityDBContextTest()
        {
            UnityDBContext context = new UnityDBContext();
            foreach (var art in context.Articles)
            {
                Console.WriteLine("——————————————");
                Console.WriteLine($"文章：{art.Title} ({art.ArticleID})");
                foreach (var cnt in art.Contents)
                {
                    Console.WriteLine($"\t{cnt.ImageDescription}");
                }
            }
        }
    }
}