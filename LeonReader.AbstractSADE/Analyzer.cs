using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeonReader.AbstractSADE
{
    public abstract class Analyzer : Processer, IProcesser
    {
        public Analyzer() : base() { }

        /// <summary>
        /// 扫描文章
        /// </summary>
        public abstract void Process();

    }
}
