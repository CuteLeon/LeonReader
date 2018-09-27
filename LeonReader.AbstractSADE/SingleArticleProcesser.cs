using System;

using LeonReader.Common;
using LeonReader.Model;

namespace LeonReader.AbstractSADE
{
    /// <summary>
    /// 适用于单篇文章的处理器
    /// </summary>
    public abstract class SingleArticleProcesser : Processer
    {

        //TODO: 创建只读文章内容管理对象（注意创建和释放），参照 Processer()

        public override void Process()
        {
            //把链接转换为文章实体再传入处理方法，方便子ASDE类
            if (this.ProcessWorker.IsBusy) return;

            if (this.TargetURI == null)
            {
                LogUtils.Error($"分析器使用了空的 TargetURI，From：{this.ASDESource}");
                throw new Exception($"分析器使用了空的 TargetURI，From：{this.ASDESource}");
            }

            LogUtils.Info($"开始分析文章链接：{this.TargetURI?.AbsoluteUri}，From：{this.ASDESource}");
            //获取链接关联的文章对象
            Article article = this.GetArticle(this.TargetURI.AbsoluteUri, this.ASDESource);
            if (article == null)
            {
                LogUtils.Error($"未找到链接关联的文章实体：{this.TargetURI.AbsoluteUri}，From：{this.ASDESource}");
                throw new Exception($"未找到链接关联的文章实体：{this.TargetURI.AbsoluteUri}，From：{this.ASDESource}");
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

        /// <summary>
        /// 获取关联的文章实体
        /// </summary>
        /// <param name="link"></param>
        /// <param name="asdeSource"></param>
        /// <returns></returns>
        private Article GetArticle(string link, string asdeSource)
        {
            LogUtils.Debug($"获取链接关联的文章ID：{link}，Form：{asdeSource}");
            if (string.IsNullOrEmpty(link) || string.IsNullOrEmpty(asdeSource)) return default(Article);

            //TODO: 需要 BIZ 实现
            /*
            Article article = this.TargetArticleManager.Articles
                .FirstOrDefault(
                    art =>
                    art.ArticleLink == link &&
                    art.ASDESource == asdeSource
                );
            return article;
             */
            return default(Article);
        }

    }
}
