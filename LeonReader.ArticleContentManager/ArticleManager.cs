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
        UnityDBContext TargetDBContext = new UnityDBContext();

        /// <summary>
        /// 获取新文章
        /// </summary>
        /// <returns></returns>
        public IQueryable<Article> GetNewArticles(string source)
        {
            return 
                from article 
                in TargetDBContext.Articles
                where 
                    article.ASDESource == source &&
                    article.IsNew
                select article;
        }

    }
}
