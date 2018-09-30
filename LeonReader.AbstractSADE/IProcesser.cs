using System.ComponentModel;

namespace LeonReader.AbstractSADE
{
    /// <summary>
    /// 处理接口
    /// </summary>
    public interface IProcesser
    {
        /// <summary>
        /// 处理开始事件
        /// </summary>
        event DoWorkEventHandler ProcessStarted;

        /// <summary>
        /// 处理进度报告
        /// </summary>
        event ProgressChangedEventHandler ProcessReport;

        /// <summary>
        /// 处理完成事件
        /// </summary>
        event RunWorkerCompletedEventHandler ProcessCompleted;

        /// <summary>
        /// 开始处理
        /// </summary>
        void Process();

        /// <summary>
        /// 取消处理
        /// </summary>
        void Cancle();
    }
}
