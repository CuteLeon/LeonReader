using System;
using System.Collections.Generic;
using System.Linq;

using LeonReader.DataAccess;
using LeonReader.Model;
using static LeonReader.Model.Article;

namespace LeonReader.ArticleContentManager
{
    /// <summary>
    /// 文章管理器
    /// </summary>
    public class ArticleManager : IDisposable
    {

        /// <summary>
        /// 数据库交互对象
        /// </summary>
        readonly UnityDBContext TargetDBContext = new UnityDBContext();

        #region 增加文章

        /// <summary>
        /// 新增文章
        /// </summary>
        /// <param name="article">文章</param>
        public void AddArticle(Article article)
        {
            if (article == null) throw new ArgumentNullException(nameof(article));

            this.TargetDBContext.Articles.Add(article);
            this.TargetDBContext.SaveChanges();
        }

        /// <summary>
        /// 新增文章
        /// </summary>
        /// <param name="articles">文章</param>
        public void AddArticles(IEnumerable<Article> articles)
        {
            if (articles == null) throw new ArgumentNullException(nameof(articles));
            if (articles.Count() == 0) return;

            this.TargetDBContext.Articles.AddRange(articles);
            this.TargetDBContext.SaveChanges();
        }

        #endregion

        #region 移除文章

        /// <summary>
        /// 移除文章
        /// </summary>
        /// <param name="article">文章</param>
        public void RemoveArticle(Article article)
        {
            if (article == null) throw new ArgumentNullException(nameof(article));

            this.TargetDBContext.Articles.Remove(article);
            this.TargetDBContext.SaveChanges();
        }

        /// <summary>
        /// 移除文章
        /// </summary>
        /// <param name="articles">文章</param>
        public void RemoveArticles(IEnumerable<Article> articles)
        {
            if (articles == null) throw new ArgumentNullException(nameof(articles));
            if (articles.Count() == 0) return;

            this.TargetDBContext.Articles.RemoveRange(articles);
            this.TargetDBContext.SaveChanges();
        }

        #endregion

        #region 查询文章

        /// <summary>
        /// 获取指定处理源的所有文章
        /// </summary>
        /// <param name="source">处理源</param>
        /// <returns></returns>
        public IQueryable<Article> GetArticles(string source)
        {
            return
                from article
                in this.TargetDBContext.Articles
                where
                    article.SADESource == source
                select article;
        }

        /// <summary>
        /// 获取指定处理源的新文章
        /// </summary>
        /// <param name="source">处理源</param>
        /// <returns></returns>
        public IQueryable<Article> GetNewArticles(string source)
        {
            return
                from article
                in this.TargetDBContext.Articles
                where
                    article.SADESource == source &&
                    article.State < ArticleStates.Exported
                select article;
        }

        /// <summary>
        /// 获取指定处理源缓存但未读的文章
        /// </summary>
        /// <param name="source">处理源</param>
        /// <returns></returns>
        public IQueryable<Article> GetCachedArticle(string source)
        {
            return
                from article
                in this.TargetDBContext.Articles
                where
                    article.SADESource == source &&
                    article.State >= ArticleStates.Exported &&
                    article.State < ArticleStates.Readed &&
                    article.ExportTime != null
                select article;
        }

        /// <summary>
        /// 获取指定处理源已读的文章
        /// </summary>
        /// <param name="source">处理源</param>
        /// <returns></returns>
        public IQueryable<Article> GetReadedArticles(string source)
        {
            return
                from article
                in this.TargetDBContext.Articles
                where
                    article.SADESource == source &&
                    article.State >= ArticleStates.Readed
                select article;
        }

        /// <summary>
        /// 检查文章是否已经存在
        /// </summary>
        /// <param name="article">文章</param>
        /// <returns></returns>
        public bool CheckArticleExist(Article article)
        {
            if (article == null) throw new ArgumentNullException(nameof(article));

            Article tempArticle = this.TargetDBContext.Articles
                .FirstOrDefault(
                    art =>
                    art.ArticleID == article.ArticleID &&
                    art.SADESource == article.SADESource
                );
            return (tempArticle != null);
        }

        /// <summary>
        /// 获取指定处理源内与链接关联的文章
        /// </summary>
        /// <param name="link">链接</param>
        /// <param name="asdeSource">处理源</param>
        /// <returns></returns>
        public Article GetArticle(string link, string asdeSource)
        {
            if (string.IsNullOrEmpty(link) || string.IsNullOrEmpty(asdeSource)) return default(Article);

            Article article = this.TargetDBContext.Articles
                .FirstOrDefault(
                    art =>
                    art.ArticleLink == link &&
                    art.SADESource == asdeSource
                );
            return article;
        }

        #endregion

        #region 文章状态操作

        /// <summary>
        /// 设置文章状态
        /// </summary>
        /// <param name="article">文章</param>
        /// <param name="state">状态</param>
        /// <returns></returns>
        public void SetArticleState(Article article, ArticleStates state)
        {
            if (article == null) return;

            article.State = state;
            this.TargetDBContext.SaveChanges();
        }

        /// <summary>
        /// 设置文章的分析时间
        /// </summary>
        /// <param name="article">文章</param>
        /// <param name="dateTime">分析时间</param>
        public void SetAnalyzeTime(Article article, DateTime dateTime)
        {
            if (article == null) return;

            article.AnalyzeTime = dateTime;
            this.TargetDBContext.SaveChanges();
        }

        /// <summary>
        /// 设置文章的下载时间
        /// </summary>
        /// <param name="article">文章</param>
        /// <param name="dateTime">下载时间</param>
        public void SetDownloadTime(Article article, DateTime dateTime)
        {
            if (article == null) return;

            article.DownloadTime = dateTime;
            this.TargetDBContext.SaveChanges();
        }

        /// <summary>
        /// 设置文章的导出时间
        /// </summary>
        /// <param name="article">文章</param>
        /// <param name="dateTime">导出时间</param>
        public void SetExportTime(Article article, DateTime dateTime)
        {
            if (article == null) return;

            article.ExportTime = dateTime;
            this.TargetDBContext.SaveChanges();
        }

        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this.TargetDBContext?.Dispose();
                }

                this.disposedValue = true;
            }
        }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            this.Dispose(true);
        }
        #endregion

    }
}
