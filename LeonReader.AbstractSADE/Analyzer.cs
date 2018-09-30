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

        protected override void PreConfigProcess(Article article)
        {
            this.TargetArticleManager.SetAnalyzeTime(article, DateTime.Now);
        }

        protected override void OnProcessCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //TODO: 设置文章状态为 已分析
        }

    }
}
