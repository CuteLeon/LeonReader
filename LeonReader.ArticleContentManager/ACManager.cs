using System;
using System.Collections.Generic;
using System.Linq;

using LeonReader.DataAccess;
using LeonReader.Model;

using static LeonReader.Model.Article;
using static LeonReader.Model.ContentItem;

namespace LeonReader.ArticleContentManager
{
    /// <summary>
    /// 文章管理器
    /// </summary>
    public class ACManager : IDisposable
    {

        /// <summary>
        /// 异步锁芯
        /// </summary>
        readonly object LockSeed = new object();

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
            lock (this.LockSeed)
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
            lock (this.LockSeed)
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
            lock (this.LockSeed)
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
            lock (this.LockSeed)
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
                from
                    article in this.TargetDBContext.Articles
                where
                    article.SADESource == source
                select
                    article;
        }

        /// <summary>
        /// 获取处理源的文章数量
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public int GetArticleCount(string source)
        {
            return this.TargetDBContext.Articles.Count(
                article => article.SADESource == source
                );
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
        /// 获取指定文章
        /// </summary>
        /// <param name="id">文章ID</param>
        /// <param name="source">处理源</param>
        /// <returns></returns>
        public Article GetArticle(string id, string source)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(source)) return default(Article);

            Article article = this.TargetDBContext.Articles
                .FirstOrDefault(
                    art =>
                    art.ArticleID == id &&
                    art.SADESource == source
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
            lock (this.LockSeed)
                this.TargetDBContext.SaveChangesAsync();
        }

        /// <summary>
        /// 移除文章指定页面的内容
        /// </summary>
        /// <param name="article"></param>
        /// <param name="pageLink"></param>
        public int RemoveContentFromPage(Article article, string pageLink)
        {
            int result = article.Contents.RemoveAll(content => content.PageLink == pageLink);
            lock (this.LockSeed)
                this.TargetDBContext.SaveChanges();
            return result;
        }

        /// <summary>
        /// 设置内容状态
        /// </summary>
        /// <param name="content"></param>
        /// <param name="state"></param>
        public void SetContentState(ContentItem content, ContentStates state)
        {
            if (content == null) return;

            content.State = state;
            lock (this.LockSeed)
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
            lock (this.LockSeed)
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
            lock (this.LockSeed)
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
            lock (this.LockSeed)
                this.TargetDBContext.SaveChanges();
        }

        #endregion

        #region 查询内容

        /// <summary>
        /// 获取文章内容列表里最后一个不为空的页面链接，不存在时返回空字符串
        /// </summary>
        /// <param name="article">文章</param>
        /// <returns></returns>
        public string GetLastContentPageLink(Article article)
        {
            if (article == null) throw new ArgumentNullException("article");

            if (article.Contents == null || article.Contents.Count == 0) return string.Empty;
            return article.Contents.LastOrDefault(
                (content) => !string.IsNullOrEmpty(content.PageLink)
                )?.PageLink ?? string.Empty;
        }

        #endregion

        #region 操作内容

        /// <summary>
        /// 清除文章的内容集合
        /// </summary>
        /// <param name="article">文章</param>
        /// <returns>影响记录数</returns>
        public int ClearContents(Article article)
        {
            if (article == null) return 0;

            int count = article.Contents.RemoveAll((x) => true);
            lock (this.LockSeed)
                this.TargetDBContext.SaveChanges();
            return count;
        }

        /// <summary>
        /// 文章增加内容
        /// </summary>
        /// <param name="article">文章</param>
        /// <param name="content">内容</param>
        public int AddContent(Article article, ContentItem content)
        {
            if (article == null) throw new ArgumentNullException(nameof(article));
            if (content == null) throw new ArgumentNullException(nameof(content));

            article.Contents.Add(content);
            lock (this.LockSeed)
                return this.TargetDBContext.SaveChanges();
        }

        /// <summary>
        /// 文章增加内容
        /// </summary>
        /// <param name="article">文章</param>
        /// <param name="contents">内容</param>
        public int AddContents(Article article, IEnumerable<ContentItem> contents)
        {
            if (article == null) throw new ArgumentNullException(nameof(article));
            if (contents == null) throw new ArgumentNullException(nameof(contents));
            if (contents.Count() == 0) return 0;

            article.Contents.AddRange(contents);
            lock (this.LockSeed)
                return this.TargetDBContext.SaveChanges();
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
