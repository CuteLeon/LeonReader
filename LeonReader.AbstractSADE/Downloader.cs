using System;
using System.ComponentModel;

using LeonReader.Common;

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

            //TODO: 设置文章状态为 正在下载
        }

        protected override void OnProcessCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //TODO: 设置文章状态为 已下载
        }

    }
}
