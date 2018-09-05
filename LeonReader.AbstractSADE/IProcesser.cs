using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        event DoWorkEventHandler ProcessStarted;// { add { } remove { } }

        /// <summary>
        /// 处理完成事件
        /// </summary>
        event RunWorkerCompletedEventHandler ProcessCompleted;// { add { } remove { } }

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
