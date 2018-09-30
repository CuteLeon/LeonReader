using System;
using System.ComponentModel;

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
            this.TargetArticleManager.SetAnalyzeTime(this.TargetArticle, DateTime.Now);
            this.TargetArticleManager.SetArticleState(this.TargetArticle, Article.ArticleStates.Analyzing);
        }

        protected override void OnProcessCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.TargetArticleManager.SetArticleState(this.TargetArticle, Article.ArticleStates.Analyzed);
        }

    }
}
