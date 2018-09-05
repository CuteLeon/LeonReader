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
        //TODO: 需要测试异步取消任务功能；
        //TODO: 需要测试触发事件功能；

        #region BackgroundWorker

        /// <summary>
        /// 任务执行线程
        /// </summary>
        protected BackgroundWorker ProcessWorker = new BackgroundWorker()
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
            ProcessWorker.DoWork += OnProcessStarted;
            ProcessWorker.RunWorkerCompleted += OnProcessCompleted;

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
        /// 处理开始
        /// </summary>
        protected virtual void OnProcessStarted(object sender, DoWorkEventArgs e)
        {
            LogHelper.Info($"处理开始：{TargetURI?.AbsoluteUri}，From：{ASDESource}");
            ProcessStarted?.Invoke(this, e);
            
            // 核心业务逻辑 ...
        }

        /// <summary>
        /// 处理完成
        /// </summary>
        protected virtual void OnProcessCompleted(object sender, RunWorkerCompletedEventArgs e)
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

            // 业务逻辑 ...
        }

    }
}
