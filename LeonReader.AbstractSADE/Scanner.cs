using System;

using LeonReader.Common;

namespace LeonReader.AbstractSADE
{

    /// <summary>
    /// 文章目录扫描器
    /// </summary>
    public abstract class Scanner : Processer
    {
        #region 关联ADE类型

        /// <summary>
        /// 关联的分析器类型
        /// </summary>
        public abstract Type AnalyzerType { get; protected set; }

        /// <summary>
        /// 关联的下载器类型
        /// </summary>
        public abstract Type DownloaderType { get; protected set; }

        /// <summary>
        /// 关联的导出器类型
        /// </summary>
        public abstract Type ExportedType { get; protected set; }

        #endregion

        /// <summary>
        /// 目录地址链接
        /// </summary>
        public abstract Uri TargetCatalogURI { get; protected set; }

        /// <summary>
        /// 扫描目录
        /// </summary>
        public string ScanDirectory { get; private set; }
            = ConfigHelper.GetConfigHelper.DownloadDirectory;

        public Scanner() : base() { }

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
            ImagePath = IOUtils.PathCombine(this.ScanDirectory, ImagePath);

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
