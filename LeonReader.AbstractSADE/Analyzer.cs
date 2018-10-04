using System;
using System.ComponentModel;
using LeonReader.ArticleContentManager;
using LeonReader.Model;

namespace LeonReader.AbstractSADE
{

    /// <summary>
    /// 文章内容分析器
    /// </summary>
    public abstract class Analyzer : SingleArticleProcesser
    {
        public Analyzer() : base() { }

        protected override void PreConfigProcesser()
        {
            ACManager.GetACManager.SetAnalyzeTime(this.TargetArticle, DateTime.Now);
            ACManager.GetACManager.SetArticleState(this.TargetArticle, Article.ArticleStates.Analyzing);
        }

        protected override void OnProcessCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ACManager.GetACManager.SetArticleState(this.TargetArticle, Article.ArticleStates.Analyzed);
        }

    }
}
