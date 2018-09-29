using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Threading;

using LeonReader.AbstractSADE;
using LeonReader.Common;
using LeonReader.Model;

namespace GamerSkySADE
{
    public class GamerSkyScanner : Scanner
    {
        /// <summary>
        /// 文章处理源
        /// </summary>
        public override string SADESource { get; protected set; } = "GamerSky-趣闻";

        /// <summary>
        /// 目标地址
        /// </summary>
        public override Uri TargetCatalogURI { get; protected set; } = new Uri(@"https://www.gamersky.com/ent/qw/");

        #region 关联的ADE类型

        /// <summary>
        /// 关联的分析器类型
        /// </summary>
        public override Type AnalyzerType { get; protected set; } = typeof(GamerSkyAnalyzer);

        /// <summary>
        /// 关联的下载器类型
        /// </summary>
        public override Type DownloaderType { get; protected set; } = typeof(GamerSkyDownloader);

        /// <summary>
        /// 关联的导出器类型
        /// </summary>
        public override Type ExportedType { get; protected set; } = typeof(GamerSkyExporter);

        #endregion

        /// <summary>
        /// 扫描 GamerSky 文章
        /// </summary>
        protected override void OnProcessStarted(object sender, DoWorkEventArgs e)
        {
            if (this.TargetCatalogURI == null)
            {
                LogUtils.Error($"GS扫描器使用了空的 TargetURI。From：{this.SADESource}");
                throw new ArgumentException($"GS扫描器使用了空的 TargetURI。From：{this.SADESource}");
            }

            LogUtils.Info($"开始扫描文章目录：{this.TargetCatalogURI?.AbsoluteUri}，From：{this.SADESource}");
            string CatalogContent = string.Empty;
            try
            {
                CatalogContent = NetUtils.GetWebPage(this.TargetCatalogURI);
            }
            catch (Exception ex)
            {
                LogUtils.Error($"获取页面内容遇到错误：{this.TargetCatalogURI.AbsoluteUri}，{ex.Message}，From：{this.SADESource}");
                throw;
            }

            if (string.IsNullOrEmpty(CatalogContent))
            {
                LogUtils.Error($"获取页面内容遇到错误：{this.TargetCatalogURI.AbsoluteUri}，From：{this.SADESource}");
                throw new ArgumentException($"获取页面内容遇到错误：{this.TargetCatalogURI.AbsoluteUri}，From：{this.SADESource}");
            }

            LogUtils.Info($"开始分析目录... ，From：{this.SADESource}");
            int ArticleCount = 0;
            //扫描目录
            foreach (var article in this.ScanArticles(CatalogContent))
            {
                ArticleCount++;
                if (this.TargetArticleManager.CheckArticleExist(article))
                {
                    LogUtils.Info($"已经存在的文章：{article.Title} ({article.ArticleID}) ：{article.ArticleLink}，From：{this.SADESource}");
                }
                else
                {
                    LogUtils.Info($"发现新文章：{article.Title} ({article.ArticleID}) ：{article.ArticleLink}，From：{this.SADESource}");
                    this.TargetArticleManager.AddArticle(article);
                }

                //下载文章预览图像
                ThreadPool.QueueUserWorkItem(
                    new WaitCallback(this.DownloadPreviewImage),
                    new Tuple<string, string>(
                        article.ImageLink,
                        article.ImageFileName
                        )
                    );

                //更新已发现的文章数
                this.OnProcessReport(ArticleCount, article);

                //允许用户取消处理
                if (this.ProcessWorker.CancellationPending) break;
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
            string CatalogContentCore = this.GetCatalogContent(catalogContent);
            if (CatalogContentCore == string.Empty)
            {
                LogUtils.Error($"目录主体内容匹配为空，From：{this.SADESource}");
                throw new ArgumentException($"目录主体内容匹配为空，From：{this.SADESource}");
            }

            //分割目录
            string[] CatalogList = this.GetCatalogList(CatalogContentCore);
            if (CatalogList.Length == 0)
            {
                LogUtils.Error($"分割目录项目失败，From：{this.SADESource}");
                throw new Exception($"分割目录项目失败，From：{this.SADESource}");
            }

            //遍历目录项
            LogUtils.Debug($"开始遍历目录项");
            foreach (string CatalogItem in CatalogList)
            {
                Article article = this.ConvertToArticle(CatalogItem);
                if (article == null) { continue; }

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
                string ArticleID = IOUtils.GetFileNameWithoutExtension(CatalogMatch.Groups["ImageLink"].Value);
                string Title = CatalogMatch.Groups["Title"].Value;
                string ArticleLink = CatalogMatch.Groups["ArticleLink"].Value;
                string ImageLink = CatalogMatch.Groups["ImageLink"].Value;
                string Description = CatalogMatch.Groups["Description"].Value;
                string PublishTime = CatalogMatch.Groups["PublishTime"].Value;
                string ImageFileName = IOUtils.GetFileName(CatalogMatch.Groups["ImageLink"].Value);

                //预处理
                if (ArticleLink.StartsWith("/")) ArticleLink = NetUtils.LinkCombine(this.TargetCatalogURI, ArticleLink);
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
                    SADESource = SADESource,
                    ScanTime = DateTime.Now,
                    IsNew = true,
                    DownloadDirectoryName = IOUtils.GetSafeDirectoryName(ArticleID),
                    ArticleFileName = IOUtils.GetSafeFileName(Title),
                };
            }
            else
            {
                LogUtils.Warn($"转换为文章实体失败，From：{this.SADESource}，内容：\n< ——————————\n{catalogItem}\n—————————— >");
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
