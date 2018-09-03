using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using LeonReader.AbstractSADE;
using LeonReader.Model;
using LeonReader.Common;

namespace GamerSkySADE
{
    public class GamerSkyScanner : Scanner
    {
        /// <summary>
        /// 文章处理源
        /// </summary>
        public override string ASDESource { get; protected set; } = "GamerSky-趣闻";

        /// <summary>
        /// 目标地址
        /// </summary>
        public new Uri TargetURI { get; protected set; } = new Uri(@"https://www.gamersky.com/ent/qw/");

        /// <summary>
        /// 扫描 GamerSky 文章
        /// </summary>
        public override void Process()
        {
            if (String.IsNullOrEmpty(TargetURI?.AbsoluteUri)) throw new Exception("目标地址为空");

            Console.WriteLine($"开始扫描目录：{TargetURI.ToString()}");
            string CatalogContent = string.Empty;
            try
            {
                CatalogContent = NetHelper.GetWebPage(TargetURI);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (string.IsNullOrEmpty(CatalogContent))
            {
                throw new Exception("获取目录内容为空");
            }

            //TODO: 这里封装为方法，并写单元测试
            //开始分析目录内容
            string CatalogPattern = "<([a-z]+)(?:(?!class)[^<>])*class=([\"']?){0}\\2[^>]*>(?>(?<o><\\1[^>]*>)|(?<-o></\\1>)|(?:(?!</?\\1).))*(?(o)(?!))</\\1>";
            CatalogPattern = string.Format(CatalogPattern, Regex.Escape("pictxt contentpaging"));
            CatalogContent = new Regex(CatalogPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline).Match(CatalogContent).Value;
            if (CatalogContent == string.Empty) throw new Exception("目录匹配为空！");

            string[] CatalogList = Regex.Split(CatalogContent, "</li>");
            if (CatalogList.Length == 0) throw new Exception("获取目录数据失败！");

            //这里封装为方法，并写单元测试
            CatalogPattern = "<a href.*?=.*?\"(?<ArticleLink>.+?)\".*?target=.*?\"_blank\">.*?<img src.*?=.*?\"(?<ImageLink>.+?)\" alt.*?title=\"(?<Title>.+?)\".*?>.*?<div Class.*?=.*?\"txt\".*?>(?<Description>.+?)</div>.*?<div Class.*?=.*?\"time\".*?>(?<PublishTime>.+?)</div>.*?<div.*?>";
            Regex CatalogRegex = new Regex(CatalogPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            foreach (string CatalogItem in CatalogList)
            {
                Match CatalogMatch = CatalogRegex.Match(CatalogItem);
                if (CatalogMatch.Success)
                {
                    //TODO: 这里封装为方法，并写单元测试
                    string ArticleID = IOHelper.GetFileNameWithoutExtension(CatalogMatch.Groups["ImageLink"].Value);
                    Article article = TargetDBContext.Articles.FirstOrDefault(art => art.ArticleID==ArticleID && art.ASDESource == ASDESource);
                    if (article != null)
                    {
                        Console.WriteLine($"已经存在的文章：{ArticleID}");
                    }
                    else
                    {
                        Console.WriteLine($"发现新文章：{ArticleID}");
                        string Title = CatalogMatch.Groups["Title"].Value;
                        string ArticleLink = CatalogMatch.Groups["ArticleLink"].Value;
                        string ImageLink = CatalogMatch.Groups["ImageLink"].Value;
                        string Description = CatalogMatch.Groups["Description"].Value;
                        string PublishTime = CatalogMatch.Groups["PublishTime"].Value;
                        string ImageFileName = IOHelper.GetFileName(CatalogMatch.Groups["ImageLink"].Value);

                        //预处理
                        if (ArticleLink.StartsWith("/")) ArticleLink = NetHelper.LinkCombine(TargetURI, ArticleLink);
                        Title = Title.Replace("'","");
                        Description = Description.Replace("'", "");

                        //创建对象
                        article = new Article()
                        {
                            Title = Title,
                            ArticleLink = ArticleLink,
                            ImageLink = ImageLink,
                            Description = Description,
                            PublishTime = PublishTime,
                            ImageFileName = ImageFileName,
                            ArticleID = ArticleID,
                            ASDESource = ASDESource,
                            ScanTime = DateTime.Now,
                            IsNew = true,
                        };
                        article.DownloadDirectory = IOHelper.PathCombine(ConfigHelper.GetConfigHelper.DownloadDirectory, ArticleID);
                        article.ArticleFilePath = IOHelper.PathCombine(article.DownloadDirectory, Title);

                        TargetDBContext.Articles.Add(article);
                        TargetDBContext.SaveChanges();
                    }
                }
            }
        }

    }
}
