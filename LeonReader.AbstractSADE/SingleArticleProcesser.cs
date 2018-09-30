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
        /// 关联的文章对象
        /// </summary>
        public Article Article { get; protected set; }

        /// <summary>
        /// 目标文章内容管理对象
        /// </summary>
        public ContentManager TargetContentManager { get; protected set; }

        public SingleArticleProcesser() : base()
        {
            this.TargetContentManager = new ContentManager();
        }

        /// <summary>
        /// 开始处理
        /// </summary>
        public virtual void Process(Article argument)
        {
            if (argument == null) throw new ArgumentNullException("argument");
            if (this.ProcessWorker.IsBusy) return;

            LogUtils.Info($"开始分析文章链接：{argument.ArticleLink}，From：{this.SADESource}");

            this.Article = argument;
            this.PreConfigProcess(argument);
            this.ProcessWorker.RunWorkerAsync(argument);
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
