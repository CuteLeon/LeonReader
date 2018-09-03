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
        /// <summary>
        /// 文章处理源
        /// </summary>
        public override string ASDESource { get; protected set; } = "GamerSky-趣闻";

        /// <summary>
        /// 目标地址
        /// </summary>
        public new Uri TargetURI { get; protected set; } = new Uri(@"https://www.gamersky.com/ent/qw/");

        public override void Process()
        {
            Console.WriteLine("开始分析文章...");
            foreach (var article in TargetDBContext.Articles.Where(art => art.ASDESource == ASDESource))
            {
                Console.WriteLine($"文章链接：{article.ArticleLink}");

                article.AnalyzeTime = DateTime.Now;
                TargetDBContext.SaveChanges();
            }
        }
    }
}
