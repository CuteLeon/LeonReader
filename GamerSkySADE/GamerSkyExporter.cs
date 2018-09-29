using System;
using System.ComponentModel;
using System.IO;
using System.Text;

using LeonReader.AbstractSADE;
using LeonReader.Common;
using LeonReader.Model;

namespace GamerSkySADE
{
    public class GamerSkyExporter : Exporter
    {

        /// <summary>
        /// 内容计数
        /// </summary>
        private int ContentCount = 0;

        /// <summary>
        /// 文章处理源
        /// </summary>
        public override string SADESource { get; protected set; } = "GamerSky-趣闻";

        protected override void OnProcessStarted(object sender, DoWorkEventArgs e)
        {
            if (!(e.Argument is Article article)) throw new ArgumentException($"GS导出器文章参数为空");
            if (article.Contents == null) throw new ArgumentNullException($"GS导出器文章链接为空");
            if (string.IsNullOrEmpty(article.DownloadDirectoryName) || string.IsNullOrEmpty(article.ArticleFileName))
                throw new ArgumentNullException($"GS导出器导出目录或文件名为空");

            //检查文章导出目录
            try
            {
                this.CheckDownloadDirectory(this.ExportDirectory);
            }
            catch (Exception ex)
            {
                LogUtils.Error($"检查文章导出目录失败：{ex.Message}，From：{this.SADESource}");
                throw;
            }

            //初始化
            this.ContentCount = 0;
            this.TargetArticleManager.SetExportTime(article, DateTime.Now);

            //导出文章内容
            try
            {
                this.ExportArticle(article);
                //全部导出后保存文章内容数据
                LogUtils.Info($"文章导出完成：{this.ExportPath} (From：{this.SADESource})");
            }
            catch (Exception ex)
            {
                LogUtils.Error($"文章导出失败：{ex.Message}，{this.ExportPath}，From：{this.SADESource}");
                throw;
            }

            e.Result = this.ExportPath;
        }

        /// <summary>
        /// 导出文章
        /// </summary>
        ///<param name="article">文章实体</param>
        private void ExportArticle(Article article)
        {
            if (article == null ||
                article.Contents == null ||
                string.IsNullOrEmpty(article.DownloadDirectoryName) ||
                string.IsNullOrEmpty(article.ArticleFileName))
                throw new ArgumentException("导出文章传入的参数为空");

            using (StreamWriter ArticleStream = new StreamWriter(this.ExportPath, false, Encoding.UTF8))
            {
                try
                {
                    ArticleStream.Write("<html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /><style>{0}</style></head><body style=\"width:70%;margin:0 auto\"><center><pre><h1><strong>{1}</strong></h1></pre>\n", "body{text-align: center} img{widows: auto!important;height: auto!important;}", article.Title);

                    foreach (var content in article.Contents)
                    {
                        ArticleStream.WriteLine(@"<img class=""lazyimage"" onclick=""click2load(this)"" data-src="".\{0}"" alt=""点击以重新加载图片    {1}""><br>{2}<br><hr>", content.ImageFileName, content.ImageLink, content.ImageDescription);
                        //触发事件更新已导出的图像计数
                        this.ContentCount++;
                        this.OnProcessReport(this.ContentCount, article.Contents.Count);

                        //允许用户取消处理，用户取消后仍会写入后续的结束标记和JS
                        if (this.ProcessWorker.CancellationPending) break;
                    }
                    ArticleStream.Write("<<<< 文章结束 >>>></center>\n{0}\n</body></html>", GSResource.LazyLoadJS);
                    LogUtils.Debug("文章组装完成：{0}", article.ArticleLink);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    ArticleStream?.Close();
                }
            }
        }

        /// <summary>
        /// 检查导出目录
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

    }
}
