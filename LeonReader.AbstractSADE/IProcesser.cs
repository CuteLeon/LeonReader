using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeonReader.AbstractSADE
{
    public interface IProcesser //TODO: 实现事件<T>
    {
        /* TODO: 实现三个事件：开始、更新进度、完成（可以泛型传入自定义的事件参数类型，或者直接使用 BackGroundWorker 的...）
        event EventHandler<T> Finish;
         */
        void Process();
    }
}
