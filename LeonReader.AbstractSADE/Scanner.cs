using LeonReader.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeonReader.AbstractSADE
{

    /// <summary>
    /// 文章目录扫描器
    /// </summary>
    public abstract class Scanner : Processer
    {
        public Scanner() : base() { }
        
        /// <summary>
        /// 检查文章是否已经存在
        /// </summary>
        /// <param name="article">文章实体</param>
        /// <returns></returns>
        protected virtual bool CheckArticleExist(Article article)
        {
            Article tempArticle = TargetDBContext.Articles
                .FirstOrDefault(
                    art =>
                    art.ArticleID == article.ArticleID &&
                    art.ASDESource == article.ASDESource
                );
            return (tempArticle != null);
        }

    }
}
