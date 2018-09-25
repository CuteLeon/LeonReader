using System;
using System.Linq;

using LeonReader.Model;

namespace LeonReader.ArticleContentManager
{
    /// <summary>
    /// 文章管理器
    /// </summary>
    public class ArticleManager
    {
        /// <summary>
        /// 数据库交互对象
        /// </summary>
        readonly UnityDBContext TargetDBContext = new UnityDBContext();

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
            article.IsNew = false;
            article.ExportTime = DateTime.Now;
            this.TargetDBContext.SaveChanges();
        }

    }
}
