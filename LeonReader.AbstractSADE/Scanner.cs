using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeonReader.AbstractSADE
{
    /* TODO: 实现事件
    public class ScannerEventArgs : EventArgs
    {
    }
     */

    public abstract class Scanner : Processer,IProcesser //TODO: 实现事件 IProcesser<ScannerEventArgs>
    {
        public Scanner() : base() { }

        /// <summary>
        /// 扫描文章
        /// </summary>
        public abstract void Process();
        
    }
}
