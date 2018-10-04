using System;
using System.ComponentModel;
using LeonReader.ArticleContentManager;
using LeonReader.Common;
using LeonReader.Model;

namespace LeonReader.AbstractSADE
{
    /// <summary>
    /// 文章内容下载器
    /// </summary>
    public abstract class Downloader : SingleArticleProcesser
    {
        /// <summary>
        /// 下载目录
        /// </summary>
        public string DownloadDirectory { get; private set; }

        public Downloader() : base() { }

        protected override void PreConfigProcesser()
        {
            ACManager.GetACManager.SetDownloadTime(this.TargetArticle, DateTime.Now);

            this.DownloadDirectory = IOUtils.PathCombine(
                ConfigHelper.GetConfigHelper.DownloadDirectory,
                this.TargetArticle.DownloadDirectoryName
                );

            ACManager.GetACManager.SetArticleState(this.TargetArticle, Article.ArticleStates.Downloading);
        }

        protected override void OnProcessCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ACManager.GetACManager.SetArticleState(this.TargetArticle, Article.ArticleStates.Downloaded);
        }

    }
}
