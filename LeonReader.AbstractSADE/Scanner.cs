using System;
using System.Linq;

using LeonReader.Common;
using LeonReader.Model;

namespace LeonReader.AbstractSADE
{

    /// <summary>
    /// 文章目录扫描器
    /// </summary>
    public abstract class Scanner : Processer
    {
        /// <summary>
        /// 扫描目录
        /// </summary>
        public string ScanDirectory { get; private set; } 
            = ConfigHelper.GetConfigHelper.DownloadDirectory;

        public Scanner() : base() { }
        
        /// <summary>
        /// 检查文章是否已经存在
        /// </summary>
        /// <param name="article">文章实体</param>
        /// <returns></returns>
        protected virtual bool CheckArticleExist(Article article)
        {
            Article tempArticle = TargetDBContext.Articles
                .FirstOrDefault(
                    art =>
                    art.ArticleID == article.ArticleID &&
                    art.ASDESource == article.ASDESource
                );
            return (tempArticle != null);
        }

        /// <summary>
        /// 下载文章预览图像
        /// </summary>
        /// <param name="TupleOfLinkAndPath">图像链接和路径元组</param>
        protected virtual void DownloadPreviewImage(object TupleOfLinkAndPath)
        {
            string ImageLink = (TupleOfLinkAndPath as Tuple<string, string>).Item1;
            string ImagePath = (TupleOfLinkAndPath as Tuple<string, string>).Item2;
            if (string.IsNullOrEmpty(ImageLink) || string.IsNullOrEmpty(ImagePath))
            {
                LogUtils.Error($"下载文章预览图像遇到空的图像链接或图像文件名称：{ImageLink}，{ImagePath}");
                return;
            }
            ImagePath = IOUtils.PathCombine(ScanDirectory, ImagePath);

            if (!IOUtils.FileExists(ImagePath) || IOUtils.GetFileSize(ImagePath) == 0)
                try
                {
                    NetUtils.DownloadWebFile(ImageLink, ImagePath);
                }
                catch (Exception ex)
                {
                    LogUtils.Error($"下载文章预览图像遇到异常：{ImageLink} => {ImagePath}：{ex.Message}");
                }
        }
    }
}
