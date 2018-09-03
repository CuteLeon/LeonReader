using LeonReader.AbstractSADE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSkySADE
{
    public class GamerSkyAnalyzer : Analyzer
    {
        public override void Process()
        {
            Console.WriteLine("开始分析文章...");
            foreach (var article in TargetDBContext.Articles)
            {
                Console.WriteLine($"文章链接：{article.ArticleLink}");
            }
        }
    }
}
