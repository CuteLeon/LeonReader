﻿using System;
using System.ComponentModel;

using LeonReader.AbstractSADE;
using LeonReader.Common;
using LeonReader.Model;

namespace GamerSkySADE
{
    public class GamerSkyDownloader : Downloader
    {
        /// <summary>
        /// 内容计数
        /// </summary>
        private int ContentCount = 0;

        /// <summary>
        /// 下载失败计数
        /// </summary>
        private int FailedCount = 0;

        /// <summary>
        /// 文章处理源
        /// </summary>
        public override string ASDESource { get; protected set; } = "GamerSky-趣闻";

        protected override void OnProcessStarted(object sender, DoWorkEventArgs e)
        {
            if (!(e.Argument is Article article)) throw new Exception($"未找到链接关联的文章实体：{this.TargetURI.AbsoluteUri}");

            //检查文章下载目录
            try
            {
                this.CheckDownloadDirectory(this.DownloadDirectory);
            }
            catch (Exception ex)
            {
                LogUtils.Error($"检查文章下载目录失败：{ex.Message}，From：{this.ASDESource}");
                throw;
            }
            
            //TODO: 需要 BIZ 实现
            /*
            //初始化
            this.ContentCount = 0;
            this.FailedCount = 0;
            article.DownloadTime = DateTime.Now;
            this.TargetArticleManager.SaveChanges();

            //开始任务
            foreach (var content in article.Contents)
            {
                //下载文章内容
                try
                {
                    this.DownloadContent(content, this.DownloadDirectory);
                    this.ContentCount++;
                }
                catch (Exception ex)
                {
                    this.FailedCount++;
                    LogUtils.Error($"文章内容下载失败：{ex.Message}，From：{this.ASDESource}");
                }

                //触发事件更新已下载的图像计数
                this.OnProcessReport(this.ContentCount, this.FailedCount);

                //允许用户取消处理
                if (this.ProcessWorker.CancellationPending) break;
            }

            //全部下载后保存文章内容数据
            LogUtils.Info($"文章下载完成：{this.TargetURI.AbsoluteUri} (From：{this.ASDESource})");
             */
        }

        /// <summary>
        /// 检查下载目录
        /// </summary>
        private void CheckDownloadDirectory(string directory)
        {
            if (!IOUtils.DirectoryExists(directory))
            {
                try
                {
                    IOUtils.CreateDirectory(directory);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// 下载内容
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="directory">下载目录</param>
        private void DownloadContent(ContentItem content, string directory)
        {
            if (content == null) throw new Exception($"空的文章内容对象，From：{this.ASDESource}");
            if (string.IsNullOrEmpty(content.ImageFileName) || string.IsNullOrEmpty(content.ImageLink))
                throw new Exception($"文章内容的图像路径或链接为空，From：{this.ASDESource}");

            string ContentPath = IOUtils.PathCombine(directory, content.ImageFileName);
            string ContentLink = content.ImageLink;

            if (IOUtils.FileExists(ContentPath))
            {
                if (IOUtils.GetFileSize(ContentPath) > 0)
                {
                    //已经存在且长度大于0的文件直接跳过
                    return;
                }
                else
                {
                    //尝试删除存在的空文件，删不掉也无所谓，随缘
                    try { IOUtils.DeleteFile(ContentPath); } catch { }
                }
            }

            try
            {
                NetUtils.DownloadWebFile(ContentLink, ContentPath);
                LogUtils.Error($"文章内容下载成功：{ContentLink}，{ContentPath}，From：{this.ASDESource}");
            }
            catch (Exception ex)
            {
                LogUtils.Error($"文章内容下载失败：{ex.Message}，{ContentLink}，{ContentPath}，From：{this.ASDESource}");
            }
        }

    }
}
