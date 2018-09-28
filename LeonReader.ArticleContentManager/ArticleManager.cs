using System;
using System.Collections.Generic;
using System.Linq;

using LeonReader.DataAccess;
using LeonReader.Model;

namespace LeonReader.ArticleContentManager
{
    /// <summary>
    /// 文章管理器
    /// </summary>
    public class ArticleManager : IDisposable
    {

        /// <summary>
        /// 新增文章
        /// </summary>
        /// <param name="article"></param>
        public void AddArticle(Article article)
        {
            if (article == null) throw new ArgumentNullException(nameof(article));

            this.TargetDBContext.Articles.Add(article);
            this.TargetDBContext.SaveChanges();
        }

        /// <summary>
        /// 新增文章
        /// </summary>
        /// <param name="articles"></param>
        public void AddArticles(IEnumerable<Article> articles)
        {
            if (articles == null) throw new ArgumentNullException(nameof(articles));

            this.TargetDBContext.Articles.AddRange(articles);
            this.TargetDBContext.SaveChanges();
        }

        /// <summary>
        /// 移除文章
        /// </summary>
        /// <param name="article"></param>
        public void RemoveArticle(Article article)
        {
            if (article == null) throw new ArgumentNullException(nameof(article));

            this.TargetDBContext.Articles.Remove(article);
            this.TargetDBContext.SaveChanges();
        }

        /// <summary>
        /// 移除文章
        /// </summary>
        /// <param name="articles"></param>
        public void RemoveArticles(IEnumerable<Article> articles)
        {
            if (articles == null) throw new ArgumentNullException(nameof(articles));

            this.TargetDBContext.Articles.RemoveRange(articles);
            this.TargetDBContext.SaveChanges();
        }

        /// <summary>
        /// 数据库交互对象
        /// </summary>
        readonly UnityDBContext TargetDBContext = new UnityDBContext();

        /// <summary>
        /// 获取文章
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public IQueryable<Article> GetArticles(string source)
        {
            return
                from article
                in this.TargetDBContext.Articles
                where
                    article.ASDESource == source
                select article;
        }

        /// <summary>
        /// 获取新文章
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public IQueryable<Article> GetNewArticles(string source)
        {
            return
                from article
                in this.TargetDBContext.Articles
                where
                    article.ASDESource == source &&
                    article.IsNew
                select article;
        }

        /// <summary>
        /// 获取扫描过但未下载的文章
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public IQueryable<Article> GetScanedArticle(string source)
        {
            return
                from article
                in this.TargetDBContext.Articles
                where
                    article.ASDESource == source &&
                    !article.IsNew &&
                    article.ExportTime == null
                select article;
        }

        /// <summary>
        /// 获取扫描过并下载过的文章
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public IQueryable<Article> GetDownloadedArticles(string source)
        {
            return
                from article
                in this.TargetDBContext.Articles
                where
                    article.ASDESource == source &&
                    !article.IsNew &&
                    article.ExportTime != null
                select article;
        }

        /// <summary>
        /// 将文章置为已读
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public void SetArticleReaded(Article article)
        {
            if (article == null) return;

            article.IsNew = false;
            article.ExportTime = DateTime.Now;
            this.TargetDBContext.SaveChanges();
        }

        /// <summary>
        /// 检查文章是否已经存在
        /// </summary>
        /// <param name="article">文章实体</param>
        /// <returns></returns>
        public bool CheckArticleExist(Article article)
        {
            if (article == null) return false;

            Article tempArticle = this.TargetDBContext.Articles
                .FirstOrDefault(
                    art =>
                    art.ArticleID == article.ArticleID &&
                    art.ASDESource == article.ASDESource
                );
            return (tempArticle != null);
        }

        /// <summary>
        /// 获取链接关联的文章实体
        /// </summary>
        /// <param name="link"></param>
        /// <param name="asdeSource"></param>
        /// <returns></returns>
        public Article GetArticle(string link, string asdeSource)
        {
            if (string.IsNullOrEmpty(link) || string.IsNullOrEmpty(asdeSource)) return default(Article);

            Article article = this.TargetDBContext.Articles
                .FirstOrDefault(
                    art =>
                    art.ArticleLink == link &&
                    art.ASDESource == asdeSource
                );
            return article;
        }

        /// <summary>
        /// 清除文章的内容
        /// </summary>
        /// <param name="article"></param>
        /// <returns>影响记录数</returns>
        public int ClearContents(Article article)
        {
            if (article == null) return 0;

            int count = article.Contents.RemoveAll((x) => true);
            this.TargetDBContext.SaveChanges();
            return count;
        }

        /// <summary>
        /// 设置文章的分析时间
        /// </summary>
        /// <param name="article"></param>
        /// <param name="dateTime"></param>
        public void SetAnalyzeTime(Article article, DateTime dateTime)
        {
            if (article == null) return;

            article.AnalyzeTime = dateTime;
            this.TargetDBContext.SaveChanges();
        }

        /// <summary>
        /// 设置文章的下载时间
        /// </summary>
        /// <param name="article"></param>
        /// <param name="dateTime"></param>
        public void SetDownloadTime(Article article, DateTime dateTime)
        {
            if (article == null) return;

            article.DownloadTime = dateTime;
            this.TargetDBContext.SaveChanges();
        }

        /// <summary>
        /// 设置文章的导出时间
        /// </summary>
        /// <param name="article"></param>
        /// <param name="dateTime"></param>
        public void SetExportTime(Article article, DateTime dateTime)
        {
            if (article == null) return;

            article.ExportTime = dateTime;
            this.TargetDBContext.SaveChanges();
        }

        /// <summary>
        /// 文章增加内容
        /// </summary>
        /// <param name="article"></param>
        /// <param name="content"></param>
        public void AddContent(Article article, ContentItem content)
        {
            if (article == null || content == null) return;

            article.Contents.Add(content);
            this.TargetDBContext.SaveChanges();
        }

        /// <summary>
        /// 文章增加内容
        /// </summary>
        /// <param name="article"></param>
        /// <param name="contents"></param>
        public void AddContents(Article article, IEnumerable<ContentItem> contents)
        {
            if (article == null || contents == null || contents.Count() == 0) return;

            article.Contents.AddRange(contents);
            this.TargetDBContext.SaveChanges();
        }

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
