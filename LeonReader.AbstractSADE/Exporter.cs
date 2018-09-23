using LeonReader.Common;
using LeonReader.Model;

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

        /// <summary>
        /// 导出文件扩展名
        /// </summary>
        protected abstract string Extension { get; set; }

        public Exporter() : base() { }

        protected override void PreConfigProcess(Article article)
        {
            ExportDirectory = IOUtils.PathCombine(
                ConfigHelper.GetConfigHelper.DownloadDirectory,
                article.DownloadDirectoryName
                );
            ExportPath = IOUtils.PathCombine(
                ConfigHelper.GetConfigHelper.DownloadDirectory,
                article.DownloadDirectoryName,
                article.ArticleFileName
                );
            ExportPath += Extension.StartsWith(".") ? Extension : "." + Extension;
        }
    }
}
