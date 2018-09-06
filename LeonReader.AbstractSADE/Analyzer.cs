using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeonReader.AbstractSADE
{
    /// <summary>
    /// 文章内容分析器
    /// </summary>
    public abstract class Analyzer : Processer
    {
        /* 没必要，一个事件可以传送两个数据
        /// <summary>
        /// 分析状态
        /// </summary>
        public struct AnalyzeState
        {
            /// <summary>
            /// 页面计数
            /// </summary>
            public int PageCount;

            /// <summary>
            /// 内容计数
            /// </summary>
            public int ContentCount;

            /// <summary>
            /// 构造分析状态
            /// </summary>
            /// <param name="pageCount">页面计数</param>
            /// <param name="contentCount">内容计数</param>
            public AnalyzeState(int pageCount, int contentCount)
            {
                PageCount = pageCount;
                ContentCount = contentCount;
            }
        }
         */

        public Analyzer() : base() { }

    }
}
