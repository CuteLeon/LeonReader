using System;

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
