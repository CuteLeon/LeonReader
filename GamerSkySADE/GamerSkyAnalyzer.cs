using LeonReader.AbstractSADE;
using LeonReader.Common;
using LeonReader.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GamerSkySADE
{
    public class GamerSkyAnalyzer : Analyzer
    {
        /// <summary>
        /// 页面计数
        /// </summary>
        private int PageCount = 0;

        /// <summary>
        /// 内容计数
        /// </summary>
        private int ContentCount = 0;

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
            //TODO: 每个 Analyzer 每次只分析一篇文章，这个循环放至外层
            Console.WriteLine("开始分析文章...");

            foreach (var article in TargetDBContext.Articles.Where(art => art.ASDESource == ASDESource))
            {
                Console.WriteLine($"文章链接：{article.ArticleLink}");
                article.Contents.Clear();
                article.AnalyzeTime = DateTime.Now;
                TargetDBContext.SaveChanges();

                PageCount = 0;
                ContentCount = 0;
                foreach (var content in AnalyseArticle(article.ArticleLink))
                {
                    Console.WriteLine($"{content.ID}, {content.ImageLink}, {content.ImageDescription}");
                    article.Contents.Add(content);
                }

                //保存页面内容数据
                TargetDBContext.SaveChanges();
            }
        }

        /// <summary>
        /// 分析文章
        /// </summary>
        private IEnumerable<ContentItem> AnalyseArticle(string PageAddress)
        {
            if (string.IsNullOrEmpty(PageAddress)) throw new Exception("分析文章遇到错误，页面地址为空");

            //页面内容，页数导航内容
            string PageContent = string.Empty, PaginationString = string.Empty;
            try
            {
                PageContent = NetHelper.GetWebPage(TargetURI);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (string.IsNullOrEmpty(PageContent))
            {
                throw new Exception("获取页面内容为空");
            }
            
            //页面计数自加
            PageCount++;

            //TODO: 这里封装为方法，并写单元测试
            string ContentPattern = string.Format("<([a-z]+)(?:(?!class)[^<>])*class=([\"']?){0}\\2[^>]*>(?>(?<o><\\1[^>]*>)|(?<-o></\\1>)|(?:(?!</?\\1).))*(?(o)(?!))</\\1>", Regex.Escape("Mid2L_con"));
            Regex ContentRegex = new Regex(ContentPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            Match ContentMatch = ContentRegex.Match(PageContent);
            PageContent = ContentMatch.Value;
            if (PageContent == string.Empty)
            {
                Console.WriteLine($"页面主体部分匹配失败（已分析 {PageCount} 页）：{PageAddress}");
                //throw new Exception("文章页面匹配失败");
            }

            //TODO: 这里封装为方法，并写单元测试
            int PaginationStart = PageContent.IndexOf("<!--{pe.begin.pagination}-->");
            if (PaginationStart < 0)
            {
                Console.WriteLine($"无法分割文章内容和页导航栏（已分析 {PageCount} 页）：{PageAddress}");
                yield break;
            }
            
            //分割文章内容和换页按钮(上下行位置不可交换)
            PaginationString = PageContent.Substring(PaginationStart);
            PageContent = PageContent.Substring(0, PaginationStart);
            if (PageContent == string.Empty)
            {
                Console.WriteLine($"文章内容区域匹配为空（已分析 {PageCount} 页）：{PageAddress}");
            }
            else
            {
                //TODO: 这里封装为方法，并写单元测试
                //分析文章内容
                string[] ContentItems = Regex.Split(PageContent, "</p>");
                string Link = string.Empty;
                string Description = string.Empty;
                string ContentWithoutNL = string.Empty;

                foreach (string Content in ContentItems)
                {
                    //TODO: 这里封装为方法，并写单元测试
                    //去除换行符
                    ContentWithoutNL = Content.Replace("\n", "");

                    //使用第一种匹配策略获取图像路径
                    ContentPattern = "<a.*?shtml\\?(?<ImageLink>.+?)\"";
                    ContentRegex = new Regex(ContentPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    ContentMatch = ContentRegex.Match(ContentWithoutNL);
                    Link = string.Empty;
                    Description = string.Empty;
                    if (ContentMatch.Success)
                    {
                        //匹配成功
                        Link = ContentMatch.Groups["ImageLink"].Value as string;
                    }
                    else
                    {
                        //匹配失败，切换策略获取图像路径
                        ContentPattern = "<img.*?src=\"(?<ImageLink>.+?)\"";
                        ContentRegex = new Regex(ContentPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                        ContentMatch = ContentRegex.Match(ContentWithoutNL);
                        if (ContentMatch.Success)
                        {
                            //匹配成功
                            Link = ContentMatch.Groups["ImageLink"].Value as string;
                        }
                        else
                        {
                            //再次匹配失败，自暴自弃~
                            continue;
                        }
                    }
                    //获取图像描述
                    string[] TempDescription = Regex.Split(ContentWithoutNL, "<br>");
                    Description = TempDescription.Length > 1 ? TempDescription.Last() : "";

                    ContentCount++;
                    //TODO: 触发时间更新已分析的页面数和图像数 ContentCount & PageCount
                    ContentItem content = new ContentItem(Description, Link, IOHelper.GetFileName(Link));
                    //抛出对象
                    yield return content;
                }
            }

            if (PaginationString == string.Empty)
            {
                Console.WriteLine($"文章内容区域匹配为空（已分析 {PageCount} 页）：{PageAddress}");
                yield break;
            }
            else
            {
                //TODO: 这里封装为方法，并写单元测试
                //分析下一页
                ContentPattern = "<a href=\"(?<NextPageLink>.+?)\">下一页</a>";
                ContentRegex = new Regex(ContentPattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                ContentMatch = ContentRegex.Match(PaginationString);

                if (ContentMatch.Success)
                {
                    //递归分析下一页链接地址
                    AnalyseArticle(ContentMatch.Groups["NextPageLink"].Value as string);
                }
                else
                {
                    //正常的扫描结束
                    yield break;
                }
            }
        }

    }
}
