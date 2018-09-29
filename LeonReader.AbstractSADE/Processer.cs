using System;
using System.ComponentModel;

using LeonReader.ArticleContentManager;
using LeonReader.Common;

namespace LeonReader.AbstractSADE
{

    /// <summary>
    /// 抽象处理类
    /// </summary>
    public abstract class Processer : IProcesser, IDisposable
    {
        #region BackgroundWorker

        /// <summary>
        /// 任务执行线程
        /// </summary>
        protected BackgroundWorker ProcessWorker { get; private set; } = new BackgroundWorker()
        {
            WorkerReportsProgress = true,
            WorkerSupportsCancellation = true
        };

        /// <summary>
        /// 处理开始事件
        /// </summary>
        public event DoWorkEventHandler ProcessStarted;

        /// <summary>
        /// 处理完成事件
        /// </summary>
        public event RunWorkerCompletedEventHandler ProcessCompleted;

        /// <summary>
        /// 处理进度报告
        /// </summary>
        public event ProgressChangedEventHandler ProcessReport;
        /* 不要这样，否则事件的触发者是 Worker 而不是 Processer
        {
            add { ProcessWorker.ProgressChanged += value; }
            remove { ProcessWorker.ProgressChanged -= value; }
        }
         */

        /// <summary>
        /// 是否在进行异步操作
        /// </summary>
        public bool IsBusy { get => this.ProcessWorker.IsBusy; }

        #endregion
        
        /// <summary>
        /// 文章处理源
        /// </summary>
        public abstract string SADESource { get; protected set; }

        /// <summary>
        /// 目标地址
        /// </summary>
        public virtual Uri TargetURI { get; protected set; }

        /// <summary>
        /// 目标文章管理对象
        /// </summary>
        public ArticleManager TargetArticleManager { get; protected set; }

        public Processer()
        {
            this.ProcessWorker.DoWork += this.PreProcessStarted;
            this.ProcessWorker.ProgressChanged += this.PreProcessReport;
            this.ProcessWorker.RunWorkerCompleted += this.PreProcessCompleted;

            this.TargetArticleManager = new ArticleManager();
        }

        /// <summary>
        /// 注入文章地址
        /// </summary>
        /// <param name="uri">文章地址</param>
        public void SetTargetURI(Uri uri) => this.TargetURI = uri;

        /// <summary>
        /// 注入文章地址
        /// </summary>
        /// <param name="uri">文章地址</param>
        public void SetTargetURI(string uri) => this.TargetURI = new Uri(uri);

        /// <summary>
        /// 开始处理
        /// </summary>
        public virtual void Process()
        {
            if (this.ProcessWorker.IsBusy) return;
            this.ProcessWorker.RunWorkerAsync();
        }

        /// <summary>
        /// 开始处理
        /// </summary>
        public virtual void Process(object argument)
        {
            if (this.ProcessWorker.IsBusy) return;
            this.ProcessWorker.RunWorkerAsync(argument);
        }

        /// <summary>
        /// 取消处理
        /// </summary>
        public void Cancle()
        {
            if (!this.ProcessWorker.IsBusy) return;
            this.ProcessWorker.CancelAsync();
        }

        /// <summary>
        /// 处理开始预处理
        /// </summary>
        private void PreProcessStarted(object sender, DoWorkEventArgs e)
        {
            LogUtils.Info($"处理开始：{this.TargetURI?.AbsoluteUri}，From：{this.SADESource}");
            //这个事件会在异步线程触发
            ProcessStarted?.Invoke(this, e);
            //允许用户在接收处理开始事件时即取消处理
            if (e.Cancel) return;
            if (this.ProcessWorker.CancellationPending) return;
            //调用子类SADE类的方法
            LogUtils.Debug($"开始处理子SADE类的 [处理开始] 方法：{this.TargetURI?.AbsoluteUri}，From：{this.SADESource}");
            this.OnProcessStarted(this.ProcessWorker, e);
        }

        /// <summary>
        /// 处理开始（e.Argument 为关联的文章实体或 null）
        /// </summary>
        protected abstract void OnProcessStarted(object sender, DoWorkEventArgs e);

        /// <summary>
        /// 报告进度预处理
        /// </summary>
        private void PreProcessReport(object sender, ProgressChangedEventArgs e)
        {
            ProcessReport?.Invoke(this, e);
        }

        /// <summary>
        /// 报告进度
        /// </summary>
        /// <param name="progress">进度值</param>
        /// <param name="userState">其他对象</param>
        protected void OnProcessReport(int progress, object userState)
        {
            this.ProcessWorker.ReportProgress(progress, userState);
        }

        /// <summary>
        /// 处理完成预处理
        /// </summary>
        private void PreProcessCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LogUtils.Info($"处理完成：{this.TargetURI?.AbsoluteUri}，From：{this.SADESource}");
            if (e.Cancelled)
            {
                LogUtils.Error($"由用户手动取消处理：{this.TargetURI?.AbsoluteUri}，From：{this.SADESource}");
            }
            if (e.Error != null)
            {
                LogUtils.Error($"处理时遇到异常：{e.Error.Message}，{this.TargetURI?.AbsoluteUri}，From：{this.SADESource}");
            }

            //调用子类SADE类的方法
            LogUtils.Debug($"开始处理子SADE类的 [处理完成] 方法：{this.TargetURI?.AbsoluteUri}，From：{this.SADESource}");
            this.OnProcessCompleted(this.ProcessWorker, e);

            //优先内部处理完完成事件再通知外部
            ProcessCompleted?.Invoke(this, e);
        }

        /// <summary>
        /// 处理完成
        /// </summary>
        protected virtual void OnProcessCompleted(object sender, RunWorkerCompletedEventArgs e) { }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this.Cancle();
                    this.ProcessWorker.Dispose();

                    this.TargetArticleManager.Dispose();
                }

                this.disposedValue = true;
            }
        }
        
        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
