using System;

using LeonReader.ArticleContentManager;
using LeonReader.Common;
using LeonReader.Model;

namespace LeonReader.AbstractSADE
{
    /// <summary>
    /// 适用于单篇文章的处理器
    /// </summary>
    public abstract class SingleArticleProcesser : Processer
    {

        /// <summary>
        /// 目标文章内容管理对象
        /// </summary>
        public ContentManager TargetContentManager { get; protected set; }

        public SingleArticleProcesser() : base()
        {
            this.TargetContentManager = new ContentManager();
        }

        public override void Process()
        {
            //把链接转换为文章实体再传入处理方法，方便子SADE类
            if (this.ProcessWorker.IsBusy) return;

            if (this.TargetURI == null)
            {
                LogUtils.Error($"分析器使用了空的 TargetURI，From：{this.SADESource}");
                throw new Exception($"分析器使用了空的 TargetURI，From：{this.SADESource}");
            }

            LogUtils.Info($"开始分析文章链接：{this.TargetURI?.AbsoluteUri}，From：{this.SADESource}");
            //获取链接关联的文章对象
            Article article = this.TargetArticleManager.GetArticle(this.TargetURI.AbsoluteUri, this.SADESource);
            if (article == null)
            {
                LogUtils.Error($"未找到链接关联的文章实体：{this.TargetURI.AbsoluteUri}，From：{this.SADESource}");
                throw new Exception($"未找到链接关联的文章实体：{this.TargetURI.AbsoluteUri}，From：{this.SADESource}");
            }
            LogUtils.Debug($"匹配到链接关联的文章实体：{article.Title} ({article.ArticleID}) => {article.ArticleLink}");
            this.PreConfigProcess(article);
            this.ProcessWorker.RunWorkerAsync(article);
        }

        /// <summary>
        /// 处理开始前由子类配置任务
        /// </summary>
        /// <param name="article">链接关联的文章实体</param>
        protected virtual void PreConfigProcess(Article article) { }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                this.TargetContentManager.Dispose();
            }
        }
    }
}
