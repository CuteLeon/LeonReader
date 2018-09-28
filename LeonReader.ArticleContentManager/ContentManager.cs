using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeonReader.DataAccess;
using LeonReader.Model;

namespace LeonReader.ArticleContentManager
{
    /// <summary>
    /// 内容管理类
    /// </summary>
    public class ContentManager : IDisposable
    {
        /// <summary>
        /// 数据库交互对象
        /// </summary>
        readonly UnityDBContext TargetDBContext = new UnityDBContext();

        #region 文章内容操作

        /// <summary>
        /// 清除文章的内容集合
        /// </summary>
        /// <param name="article">文章</param>
        /// <returns>影响记录数</returns>
        public int ClearContents(Article article)
        {
            if (article == null) return 0;

            int count = article.Contents.RemoveAll((x) => true);
            this.TargetDBContext.SaveChanges();
            return count;
        }

        /// <summary>
        /// 文章增加内容
        /// </summary>
        /// <param name="article">文章</param>
        /// <param name="content">内容</param>
        public void AddContent(Article article, ContentItem content)
        {
            if (article == null) throw new ArgumentNullException(nameof(article));
            if (content == null) throw new ArgumentNullException(nameof(content));

            article.Contents.Add(content);
            this.TargetDBContext.SaveChanges();
        }

        /// <summary>
        /// 文章增加内容
        /// </summary>
        /// <param name="article">文章</param>
        /// <param name="contents">内容</param>
        public void AddContents(Article article, IEnumerable<ContentItem> contents)
        {
            if (article == null) throw new ArgumentNullException(nameof(article));
            if (contents == null) throw new ArgumentNullException(nameof(contents));
            if (contents.Count() == 0) return;

            article.Contents.AddRange(contents);
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
