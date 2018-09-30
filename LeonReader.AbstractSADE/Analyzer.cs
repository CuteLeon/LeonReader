using System;
using System.ComponentModel;

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
            //TODO: 设置文章状态为 正在分析
        }

        protected override void OnProcessCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //TODO: 设置文章状态为 已分析
        }

    }
}
