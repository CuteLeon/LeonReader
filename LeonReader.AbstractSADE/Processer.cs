using System;
using System.ComponentModel;

using LeonReader.Common;
using LeonReader.Model;

namespace LeonReader.AbstractSADE
{
    //TODO: SADE 只允许使用数据实体，不允许访问 DBContext，与数据库的交互通过 Biz.Manager 实现；
    //TODO: [提醒] 把ASDE子类无权处理或者重复的方法上升至ASDE抽象层

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
        /// 附加参数对象
        /// </summary>
        public object Argument { get; protected set; }

        /// <summary>
        /// 文章处理源
        /// </summary>
        public abstract string ASDESource { get; protected set; }

        /// <summary>
        /// 目标地址
        /// </summary>
        public virtual Uri TargetURI { get; protected set; }

        /// <summary>
        /// 目标数据库交互对象
        /// </summary>
        public UnityDBContext TargetDBContext { get; set; }

        public Processer()
        {
            this.ProcessWorker.DoWork += this.PreProcessStarted;
            this.ProcessWorker.ProgressChanged += this.PreProcessReport;
            this.ProcessWorker.RunWorkerCompleted += this.PreProcessCompleted;

            this.TargetDBContext = new UnityDBContext();
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

        public void Dispose()
        {
            this.TargetDBContext.Dispose();
            this.Cancle();
            this.ProcessWorker.Dispose();
        }

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
            this.Argument = argument;
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
            LogUtils.Info($"处理开始：{this.TargetURI?.AbsoluteUri}，From：{this.ASDESource}");
            //这个事件会在异步线程触发
            ProcessStarted?.Invoke(this, e);
            //允许用户在接收处理开始事件时即取消处理
            if (e.Cancel) return;
            if (this.ProcessWorker.CancellationPending) return;
            //调用子类ASDE类的方法
            LogUtils.Debug($"开始处理子ASDE类的 [处理开始] 方法：{this.TargetURI?.AbsoluteUri}，From：{this.ASDESource}");
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
            LogUtils.Info($"处理完成：{this.TargetURI?.AbsoluteUri}，From：{this.ASDESource}");
            if (e.Cancelled)
            {
                LogUtils.Error($"由用户手动取消处理：{this.TargetURI?.AbsoluteUri}，From：{this.ASDESource}");
            }
            if (e.Error != null)
            {
                LogUtils.Error($"处理时遇到异常：{e.Error.Message}，{this.TargetURI?.AbsoluteUri}，From：{this.ASDESource}");
            }

            //调用子类ASDE类的方法
            LogUtils.Debug($"开始处理子ASDE类的 [处理完成] 方法：{this.TargetURI?.AbsoluteUri}，From：{this.ASDESource}");
            this.OnProcessCompleted(this.ProcessWorker, e);

            //优先内部处理完完成事件再通知外部
            ProcessCompleted?.Invoke(this, e);
        }

        /// <summary>
        /// 处理完成
        /// </summary>
        protected virtual void OnProcessCompleted(object sender, RunWorkerCompletedEventArgs e) { }

    }
}
