using System;
using System.ComponentModel;

using LeonReader.Common;

namespace LeonReader.AbstractSADE
{
    /// <summary>
    /// 导出器
    /// </summary>
    public abstract class Exporter : SingleArticleProcesser
    {
        /// <summary>
        /// 导出目录
        /// </summary>
        public string ExportDirectory { get; private set; }

        /// <summary>
        /// 导出文件路径
        /// </summary>
        public string ExportPath { get; private set; }

        public Exporter() : base() { }

        protected override void PreConfigProcesser()
        {
            this.TargetArticleManager.SetExportTime(this.TargetArticle, DateTime.Now);

            this.ExportDirectory = IOUtils.PathCombine(
                ConfigHelper.GetConfigHelper.DownloadDirectory,
                this.TargetArticle.DownloadDirectoryName
                );
            this.ExportPath = IOUtils.PathCombine(
                ConfigHelper.GetConfigHelper.DownloadDirectory,
                this.TargetArticle.DownloadDirectoryName,
                string.Format("{0}.{1}", this.TargetArticle.ArticleFileName, ConfigHelper.GetConfigHelper.Extension)
                );

            //TODO: 设置文章状态为 正在导出
        }

        protected override void OnProcessCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //TODO: 设置文章状态为 已导出
        }
    }
}
