using LeonReader.AbstractSADE;
using LeonReader.Common;
using LeonReader.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSkySADE
{
    public class GamerSkyExporter : Exporter
    {
        /// <summary>
        /// 导出目录
        /// </summary>
        string ExportDirectory = string.Empty;

        /// <summary>
        /// 内容计数
        /// </summary>
        private int ContentCount = 0;

        /// <summary>
        /// 文章处理源
        /// </summary>
        public override string ASDESource { get; protected set; } = "GamerSky-趣闻";

        protected override void OnProcessStarted(object sender, DoWorkEventArgs e)
        {
            if (TargetURI == null)
            {
                LogHelper.Error($"导出器使用了空的 TargetURI，From：{this.ASDESource}");
                throw new Exception($"导出器使用了空的 TargetURI，From：{this.ASDESource}");
            }

            LogHelper.Info($"开始导出文章链接：{TargetURI?.AbsoluteUri}，From：{this.ASDESource}");
            //获取链接关联的文章对象
            Article article = GetArticle(TargetURI.AbsoluteUri, this.ASDESource);
            if (article == null)
            {
                LogHelper.Error($"未找到链接关联的文章实体：{TargetURI.AbsoluteUri}，From：{this.ASDESource}");
                throw new Exception($"未找到链接关联的文章实体：{TargetURI.AbsoluteUri}，From：{this.ASDESource}");
            }
            if (string.IsNullOrEmpty(article.DownloadDirectoryName)) throw new Exception("文章导出目录名称为空，无法导出");
            LogHelper.Debug($"匹配到链接关联的文章实体：{article.Title} ({article.ArticleID}) => {article.ArticleLink}");

            //组装文章导出目录
            ExportDirectory = IOHelper.PathCombine(ConfigHelper.GetConfigHelper.DownloadDirectory, article.DownloadDirectoryName);
            //检查文章导出目录
            try
            {
                CheckDownloadDirectory(ExportDirectory);
            }
            catch (Exception ex)
            {
                LogHelper.Error($"检查文章导出目录失败：{ex.Message}，From：{ASDESource}");
                throw ex;
            }

            //初始化
            ContentCount = 0;
            article.ExportTime = DateTime.Now;
            TargetDBContext.SaveChanges();

            //开始任务
            foreach (var content in article.Contents)
            {
                //导出文章内容
                try
                {
                    ExportArticle(article);
                }
                catch (Exception ex)
                {
                    LogHelper.Error($"文章导出失败：{ex.Message}，From：{article.ArticleLink}，From：{ASDESource}");
                    throw ex;
                }

                //触发事件更新已导出的图像计数
                OnProcessReport(ContentCount, article.Contents.Count);

                //允许用户取消处理
                if (ProcessWorker.CancellationPending) break;
            }

            //全部导出后保存文章内容数据
            LogHelper.Info($"文章导出完成：{TargetURI.AbsoluteUri} (From：{this.ASDESource})");
        }

        /// <summary>
        /// 导出文章
        /// </summary>
        ///<param name="article">文章实体</param>
        private void ExportArticle(Article article)
        {
            if (article == null || 
                article.Contents==null ||
                string.IsNullOrEmpty(article.DownloadDirectoryName) || 
                string.IsNullOrEmpty(article.ArticleFileName))
                throw new Exception("导出文章传入的参数为空");

            string ArticleFilePath = IOHelper.PathCombine(ExportDirectory, article.ArticleFileName);
            using (StreamWriter ArticleStream = new StreamWriter(ArticleFilePath, false, Encoding.UTF8))
            {
                try
                {
                    ArticleStream.Write("<html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /><style>{0}</style></head><body style=\"width:70%;margin:0 auto\"><center><pre><h1><strong>{1}</strong></h1></pre>\n", "body{text-align: center} img{widows: auto!important;height: auto!important;}", article.Title);
                    foreach (var content in article.Contents)
                    {
                        ArticleStream.WriteLine(@"<img class=""lazyimage"" onclick=""click2load(this)"" data-src="".\{0}"" alt=""点击以重新加载图片    {1}""><br>{2}<br><hr>", content.ImageFileName, content.ImageLink, content.ImageDescription);
                    }
                    ArticleStream.Write(@"<<<< 文章结束 >>>></center>
<script>
    function click2load(sender){
        if(sender.getAttribute('src') == null || sender.getAttribute('src') == '')
            sender.src = sender.getAttribute('data-src');
        else
            sender.removeAttribute('src');
    }
    window.onload = function(){  loadImages(); };
    if (!document.getElementsByClassName){
        document.getElementsByClassName = function(className, element){
            var children = (element || document).getElementsByTagName('*');
            var elements = new Array();
            for (var i = 0; i < children.length; i++)
            {
                var child = children[i];
                var classNames = child.className.split(' ');
                for (var j = 0; j < classNames.length; j++)
                {
                    if (classNames[j] == className)
                    {
                        elements.push(child);
                        break;
                    }
                }
            }
            return elements;
        };
    }
    var aImg = document.getElementsByClassName('lazyimage');
    var len = aImg.length;
    var n = 0;//存储图片加载到的位置，避免每次都从第一张图片开始遍历
    function loadImages() {
        var seeHeight = document.documentElement.scrollHeight;
        var scrollTop = document.body.scrollTop || document.documentElement.scrollTop;
        for (var i = n; i < len; i++)
        {
            if (aImg[i].offsetTop < seeHeight + scrollTop)
            {
                if (aImg[i].getAttribute('src') == null || aImg[i].getAttribute('src') == '')
                {
                    aImg[i].src = aImg[i].getAttribute('data-src');
                }
                n = i + 1;
            }
        }
    };
    window.onscroll = loadImages;
</script>
</body></html>");
                    LogHelper.Debug("文章组装完成：{0}", article.ImageLink);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    ArticleStream?.Close();
                }
            }
        }

        /// <summary>
        /// 获取关联的文章实体
        /// </summary>
        /// <param name="link"></param>
        /// <param name="asdeSource"></param>
        /// <returns></returns>
        private Article GetArticle(string link, string asdeSource)
        {
            LogHelper.Debug($"获取链接关联的文章ID：{link}，Form：{asdeSource}");
            if (string.IsNullOrEmpty(link) || string.IsNullOrEmpty(asdeSource)) return default(Article);

            Article article = TargetDBContext.Articles
                .FirstOrDefault(
                    art =>
                    art.ArticleLink == link &&
                    art.ASDESource == asdeSource
                );
            return article;
        }

        /// <summary>
        /// 检查导出目录
        /// </summary>
        private void CheckDownloadDirectory(string directory)
        {
            if (!IOHelper.DirectoryExists(directory))
            {
                try
                {
                    IOHelper.CreateDirectory(directory);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

    }
}
