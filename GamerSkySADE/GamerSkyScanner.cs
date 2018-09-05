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
            if (TargetURI == null)
            {
                LogHelper.Error($"扫描器使用了空的 TargetURI。From：{this.ASDESource}");
                throw new Exception($"扫描器使用了空的 TargetURI。From：{this.ASDESource}");
            }

            LogHelper.Info($"开始扫描文章目录：{TargetURI?.AbsoluteUri}，From：{this.ASDESource}");
            string CatalogContent = string.Empty;
            try
            {
                CatalogContent = NetHelper.GetWebPage(TargetURI);
            }
            catch (Exception ex)
            {
                LogHelper.Error($"获取页面内容遇到错误：{TargetURI.AbsoluteUri}，{ex.Message}，From：{this.ASDESource}");
                throw ex;
            }

            if (string.IsNullOrEmpty(CatalogContent))
            {
                LogHelper.Error($"获取页面内容遇到错误：{TargetURI.AbsoluteUri}，From：{this.ASDESource}");
                throw new Exception($"获取页面内容遇到错误：{TargetURI.AbsoluteUri}，From：{this.ASDESource}");
            }

            LogHelper.Info($"开始分析目录... ，From：{this.ASDESource}");
            //扫描目录
            foreach (var article in ScanArticles(CatalogContent))
            {
                if (CheckArticleExist(article))
                {
                    LogHelper.Info($"已经存在的文章：{article.Title} ({article.ArticleID}) ：{article.ArticleLink}，From：{this.ASDESource}");
                }
                else
                {
                    LogHelper.Info($"发现新文章：{article.Title} ({article.ArticleID}) ：{article.ArticleLink}，From：{this.ASDESource}");
                    TargetDBContext.Articles.Add(article);
                    TargetDBContext.SaveChanges();
                }
            }
        }

        /// <summary>
        /// 扫描目录
        /// </summary>
        /// <param name="catalogContent"></param>
        /// <returns></returns>
        private IEnumerable<Article> ScanArticles(string catalogContent)
        {
            //获取目录主体内容
            string CatalogContentCore = GetCatalogContent(catalogContent);
            if (CatalogContentCore == string.Empty)
            {
                LogHelper.Error($"目录主体内容匹配为空，From：{this.ASDESource}");
                throw new Exception($"目录主体内容匹配为空，From：{this.ASDESource}");
            }

            //分割目录
            string[] CatalogList = GetCatalogList(CatalogContentCore);
            if (CatalogList.Length == 0)
            {
                LogHelper.Error($"分割目录项目失败，From：{this.ASDESource}");
                throw new Exception($"分割目录项目失败，From：{this.ASDESource}");
            }

            //遍历目录项
            LogHelper.Debug($"开始遍历目录项");
            foreach (string CatalogItem in CatalogList)
            {
                Article article = ConvertToArticle(CatalogItem);
                if (article == null){continue;}

                yield return article;
            }
        }

        /// <summary>
        /// 从目录项转换成文章实体
        /// </summary>
        /// <param name="catalogItem"></param>
        /// <returns></returns>
        private Article ConvertToArticle(string catalogItem)
        {
            string CatalogPattern = "<a href.*?=.*?\"(?<ArticleLink>.+?)\".*?target=.*?\"_blank\">.*?<img src.*?=.*?\"(?<ImageLink>.+?)\" alt.*?title=\"(?<Title>.+?)\".*?>.*?<div Class.*?=.*?\"txt\".*?>(?<Description>.+?)</div>.*?<div Class.*?=.*?\"time\".*?>(?<PublishTime>.+?)</div>.*?<div.*?>";
            Regex CatalogRegex = new Regex(CatalogPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            Match CatalogMatch = CatalogRegex.Match(catalogItem);
            Article article = null;
            
            if (CatalogMatch.Success)
            {
                string ArticleID = IOHelper.GetFileNameWithoutExtension(CatalogMatch.Groups["ImageLink"].Value);
                string Title = CatalogMatch.Groups["Title"].Value;
                string ArticleLink = CatalogMatch.Groups["ArticleLink"].Value;
                string ImageLink = CatalogMatch.Groups["ImageLink"].Value;
                string Description = CatalogMatch.Groups["Description"].Value;
                string PublishTime = CatalogMatch.Groups["PublishTime"].Value;
                string ImageFileName = IOHelper.GetFileName(CatalogMatch.Groups["ImageLink"].Value);

                //预处理
                if (ArticleLink.StartsWith("/")) ArticleLink = NetHelper.LinkCombine(TargetURI, ArticleLink);
                Title = Title.Replace("'", "");
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
            }
            else
            {
                LogHelper.Warn($"转换为文章实体失败，From：{this.ASDESource}，内容：\n< ——————————\n{catalogItem}\n—————————— >");
            }
            return article;
        }

        /// <summary>
        /// 分割目录
        /// </summary>
        /// <param name="catalogContent"></param>
        /// <returns></returns>
        private string[] GetCatalogList(string catalogContent)
        {
            return Regex.Split(catalogContent, "</li>");
        }

        /// <summary>
        /// 获取目录主体内容
        /// </summary>
        /// <param name="catalogContent"></param>
        /// <returns></returns>
        private string GetCatalogContent(string catalogContent)
        {
            string CatalogPattern = "<([a-z]+)(?:(?!class)[^<>])*class=([\"']?){0}\\2[^>]*>(?>(?<o><\\1[^>]*>)|(?<-o></\\1>)|(?:(?!</?\\1).))*(?(o)(?!))</\\1>";
            CatalogPattern = string.Format(CatalogPattern, Regex.Escape("pictxt contentpaging"));
            return new Regex(CatalogPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline).Match(catalogContent).Value;
        }

    }
}
