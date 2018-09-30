using System;
using System.ComponentModel;

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
            this.TargetArticleManager.SetDownloadTime(this.TargetArticle, DateTime.Now);

            this.DownloadDirectory = IOUtils.PathCombine(
                ConfigHelper.GetConfigHelper.DownloadDirectory,
                this.TargetArticle.DownloadDirectoryName
                );

            this.TargetArticleManager.SetArticleState(this.TargetArticle, Article.ArticleStates.Downloading);
        }

        protected override void OnProcessCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.TargetArticleManager.SetArticleState(this.TargetArticle, Article.ArticleStates.Downloaded);
        }

    }
}
