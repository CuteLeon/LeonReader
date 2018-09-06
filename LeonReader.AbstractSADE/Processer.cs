using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeonReader.Common;
using LeonReader.Model;

namespace LeonReader.AbstractSADE
{
    /// <summary>
    /// 抽象处理类
    /// </summary>
    public abstract class Processer : IProcesser, IDisposable
    {

        #region BackgroundWorker

        private BackgroundWorker processWorker = new BackgroundWorker()
        {
            WorkerReportsProgress = true,
            WorkerSupportsCancellation = true
        };
        /// <summary>
        /// 任务执行线程
        /// </summary>
        protected BackgroundWorker ProcessWorker { get => processWorker; private set => processWorker = value; }

        /// <summary>
        /// 处理开始事件
        /// </summary>
        public event DoWorkEventHandler ProcessStarted;

        /// <summary>
        /// 处理完成事件
        /// </summary>
        public event RunWorkerCompletedEventHandler ProcessCompleted;

        #endregion

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
            ProcessWorker.DoWork += PreProcessStarted;
            ProcessWorker.RunWorkerCompleted += PreProcessCompleted;

            TargetDBContext = new UnityDBContext();
        }

        /// <summary>
        /// 注入文章地址
        /// </summary>
        /// <param name="uri">文章地址</param>
        public void SetTargetURI(Uri uri) => TargetURI = uri;
        
        /// <summary>
        /// 注入文章地址
        /// </summary>
        /// <param name="uri">文章地址</param>
        public void SetTargetURI(string uri) => TargetURI = new Uri(uri);
        
        public void Dispose()
        {
            TargetDBContext.Dispose();
            Cancle();
            ProcessWorker.Dispose();
        }

        /// <summary>
        /// 开始处理
        /// </summary>
        public void Process()
        {
            if (ProcessWorker.IsBusy) return;
            ProcessWorker.RunWorkerAsync();
        }

        /// <summary>
        /// 取消处理
        /// </summary>
        public void Cancle()
        {
            if (!ProcessWorker.IsBusy) return;
            ProcessWorker.CancelAsync();
        }

        /// <summary>
        /// 处理开始预处理
        /// </summary>
        private void PreProcessStarted(object sender, DoWorkEventArgs e)
        {
            LogHelper.Info($"处理开始：{TargetURI?.AbsoluteUri}，From：{ASDESource}");
            ProcessStarted?.Invoke(this, e);
            //允许用户在接收处理开始事件时即取消处理
            if (e.Cancel) return;
            if (ProcessWorker.CancellationPending) return;
            //调用子类ASDE类的方法
            LogHelper.Debug($"开始处理子ASDE类的 [处理开始] 方法：{TargetURI?.AbsoluteUri}，From：{ASDESource}");
            OnProcessStarted(ProcessWorker, e);
        }

        /// <summary>
        /// 处理开始
        /// </summary>
        protected abstract void OnProcessStarted(object sender, DoWorkEventArgs e);

        /// <summary>
        /// 处理完成预处理
        /// </summary>
        private void PreProcessCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LogHelper.Info($"处理完成：{TargetURI?.AbsoluteUri}，From：{ASDESource}");
            if (e.Cancelled)
            {
                LogHelper.Error($"由用户手动取消处理：{TargetURI?.AbsoluteUri}，From：{ASDESource}");
            }
            if (e.Error != null)
            {
                LogHelper.Error($"处理时遇到异常：{e.Error.Message}，{TargetURI?.AbsoluteUri}，From：{ASDESource}");
            }
            ProcessCompleted?.Invoke(this, e);

            //调用子类ASDE类的方法
            LogHelper.Debug($"开始处理子ASDE类的 [处理完成] 方法：{TargetURI?.AbsoluteUri}，From：{ASDESource}");
            OnProcessCompleted(ProcessWorker, e);
        }

        /// <summary>
        /// 处理完成
        /// </summary>
        protected virtual void OnProcessCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

    }
}
