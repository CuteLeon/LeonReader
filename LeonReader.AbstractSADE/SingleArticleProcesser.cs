using LeonReader.Common;
using LeonReader.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeonReader.AbstractSADE
{
    /// <summary>
    /// 适用于单篇文章的处理器
    /// </summary>
    public abstract class SingleArticleProcesser : Processer
    {
        
        public override void Process()
        {
            //把链接转换为文章实体再传入处理方法，方便子ASDE类
            if (ProcessWorker.IsBusy) return;

            if (TargetURI == null)
            {
                LogHelper.Error($"分析器使用了空的 TargetURI，From：{this.ASDESource}");
                throw new Exception($"分析器使用了空的 TargetURI，From：{this.ASDESource}");
            }

            LogHelper.Info($"开始分析文章链接：{TargetURI?.AbsoluteUri}，From：{this.ASDESource}");
            //获取链接关联的文章对象
            Article article = GetArticle(TargetURI.AbsoluteUri, this.ASDESource);
            if (article == null)
            {
                LogHelper.Error($"未找到链接关联的文章实体：{TargetURI.AbsoluteUri}，From：{this.ASDESource}");
                throw new Exception($"未找到链接关联的文章实体：{TargetURI.AbsoluteUri}，From：{this.ASDESource}");
            }
            LogHelper.Debug($"匹配到链接关联的文章实体：{article.Title} ({article.ArticleID}) => {article.ArticleLink}");
            PreConfigProcess(article);
            ProcessWorker.RunWorkerAsync(article);
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

    }
}
