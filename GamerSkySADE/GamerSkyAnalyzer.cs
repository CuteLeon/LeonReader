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
        //TODO: 使用反射完成GamerSky分析器私有方法的单元测试

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

            foreach (var article in TargetDBContext.Articles.Where(art => art.ASDESource == this.ASDESource))
            {
                Console.WriteLine($"文章链接：{article.ArticleLink}");
                article.Contents.Clear();
                article.AnalyzeTime = DateTime.Now;
                TargetDBContext.SaveChanges();

                PageCount = 0;
                ContentCount = 0;
                foreach (var content in AnalyseArticle(article.ArticleLink))
                {
                    Console.WriteLine($"接收到文章内容：{content.ID}, {content.ImageLink}, {content.ImageDescription}");
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
            Console.WriteLine($"分析文章页面：{PageAddress}");
            if (string.IsNullOrEmpty(PageAddress)) throw new Exception("分析文章遇到错误，页面地址为空");

            //页面链接队列（由递归改为循环）
            Queue<string> PageLinkQueue = new Queue<string>();
            PageLinkQueue.Enqueue(PageAddress);

            while(PageLinkQueue.Count>0)
            {
                string PageLink = PageLinkQueue.Dequeue();

                //页面内容，页数导航内容
                string ArticleContent = string.Empty, PaginationString = string.Empty;
                try
                {
                    ArticleContent = NetHelper.GetWebPage(PageLink);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                if (string.IsNullOrEmpty(ArticleContent))
                {
                    throw new Exception("获取页面内容为空");
                }
            
                //页面计数自加
                PageCount++;

                //获取文章主体内容
                ArticleContent = GetArticleContent(ArticleContent);
                if (ArticleContent == string.Empty)
                {
                    Console.WriteLine($"页面主体部分匹配失败（已分析 {PageCount} 页）：{PageLink}");
                    yield break;
                    //throw new Exception("文章页面匹配失败");
                }

                try
                {
                    Tuple<string,string> tuple = SplitContentAndPagination(ArticleContent);
                    ArticleContent = tuple.Item1;
                    PaginationString = tuple.Item2;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"分割内容和分页失败（已分析 {PageCount} 页）：{PageLink}，异常：{ex.Message}");
                }
            
                //分析页面内容
                if (ArticleContent == string.Empty)
                {
                    Console.WriteLine($"文章内容区域匹配为空（已分析 {PageCount} 页）：{PageLink}");
                }
                else
                {
                    //分割文章内容
                    string[] ContentItems = GetContentList(ArticleContent);
                    foreach (var content in ContentItems)
                    {
                        //从内容项转换为内容实体
                        ContentItem contentItem = ConvertToContentItem(content);
                        if (contentItem == null)
                        {
                            //Console.WriteLine($"转换为内容实体失败（已分析 {PageCount} 页）：{PageAddress}，内容：{content}");
                        }
                        else
                        {
                            ContentCount++;
                            yield return contentItem;
                            //TODO: 触发事件更新已分析的页面数和图像数 ContentCount & PageCount
                        }
                    }
                }

                //分析分页内容
                if (PaginationString == string.Empty)
                {
                    Console.WriteLine($"文章分页区域匹配为空，无法继续。（已分析 {PageCount} 页）：{PageLink}");
                    yield break;
                }
                else
                {
                    //分析下一页链接
                    string NextLink = GetNextLink(PaginationString);
                    if (string.IsNullOrEmpty(NextLink))
                    {
                        Console.WriteLine($"文章下一页链接为空，分析结束。（已分析 {PageCount} 页）：{PageLink}");
                    }
                    else
                    {
                        Console.WriteLine($"发现下一页链接：{NextLink}");
                        //发现新页，将新页链接入队
                        PageLinkQueue.Enqueue(NextLink);
                    }
                }
            }
        }

        /// <summary>
        /// 获取下一页链接
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        private string GetNextLink(string pagination)
        {
            string ContentPattern = "<a href=\"(?<NextPageLink>.+?)\">下一页</a>";
            Regex ContentRegex = new Regex(ContentPattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            Match ContentMatch = ContentRegex.Match(pagination);

            if (ContentMatch.Success)
            {
                //返回下一页链接
                return ContentMatch.Groups["NextPageLink"].Value as string;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取文章主体内容
        /// </summary>
        /// <param name="articleContent"></param>
        /// <returns></returns>
        private string GetArticleContent(string articleContent)
        {
            string ContentPattern = string.Format("<([a-z]+)(?:(?!class)[^<>])*class=([\"']?){0}\\2[^>]*>(?>(?<o><\\1[^>]*>)|(?<-o></\\1>)|(?:(?!</?\\1).))*(?(o)(?!))</\\1>", Regex.Escape("Mid2L_con"));
            Regex ContentRegex = new Regex(ContentPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            Match ContentMatch = ContentRegex.Match(articleContent);
            return ContentMatch.Value;
        }

        /// <summary>
        /// 分割内容和分页部分
        /// </summary>
        /// <param name="page"></param>
        private Tuple<string, string> SplitContentAndPagination(string page)
        {
            int PaginationStart = page.IndexOf("<!--{pe.begin.pagination}-->");
            if (PaginationStart < 0) throw new Exception("未找到分割关键词");

            return new Tuple<string, string>(page.Substring(0, PaginationStart), page.Substring(PaginationStart));
        }

        /// <summary>
        /// 从内容项转换为内容实体
        /// </summary>
        /// <param name="contentItem"></param>
        /// <returns></returns>
        private ContentItem ConvertToContentItem(string contentItem)
        {
            string Link = string.Empty;
            string Description = string.Empty;
            string ContentWithoutNL = string.Empty;
            ContentWithoutNL = contentItem.Replace("\n", "");

            //使用第一种匹配策略获取图像路径
            string ContentPattern = "<a.*?shtml\\?(?<ImageLink>.+?)\"";
            Regex ContentRegex = new Regex(ContentPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            Match ContentMatch = ContentRegex.Match(ContentWithoutNL);

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
                    return null;
                }
            }
            //获取图像描述
            string[] TempDescription = Regex.Split(ContentWithoutNL, "<br>");
            Description = TempDescription.Length > 1 ? TempDescription.Last() : "";

            //返回对象
            ContentItem content = new ContentItem(Description, Link, IOHelper.GetFileName(Link));
            return content;
        }

        /// <summary>
        /// 分割文章目录
        /// </summary>
        /// <param name="articleContent"></param>
        /// <returns></returns>
        private string[] GetContentList(string articleContent)
        {
            return Regex.Split(articleContent, "</p>");
        }

    }
}
