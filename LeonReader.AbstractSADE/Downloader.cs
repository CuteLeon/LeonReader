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

        protected override void PreConfigProcess(Article article)
        {
            DownloadDirectory = IOUtils.PathCombine(
                ConfigHelper.GetConfigHelper.DownloadDirectory,
                article.DownloadDirectoryName
                );
        }
    }
}
