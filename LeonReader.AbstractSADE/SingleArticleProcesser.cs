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
        /// 关联的文章对象（必须是由关联的AC管理器创建的，否则无法正常保存）
        /// </summary>
        public Article TargetArticle { get; set; }

        public SingleArticleProcesser() : base() { }

        /// <summary>
        /// 开始处理
        /// </summary>
        public override void Process()
        {
            if (this.ProcessWorker.IsBusy) return;
            if (this.TargetArticle == null) throw new ArgumentNullException("文章处理器关联的文章对象为空");
            if (this.TargetACManager == null) throw new ArgumentNullException("文章处理器关联的AC管理器对象为空");

            LogUtils.Info($"开始分析文章链接：{this.TargetArticle.ArticleLink}，From：{this.SADESource}");

            this.PreConfigProcesser();
            this.ProcessWorker.RunWorkerAsync(this.TargetArticle);
        }

        /// <summary>
        /// 处理开始前由子类配置任务
        /// </summary>
        protected virtual void PreConfigProcesser() { }

    }
}
