using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LeonReader.Model;

namespace LeonReader.AbstractSADE
{
    //TODO: 每个网站板块对应一个扫描器，每个扫描器类有唯一单实例对象；
    //TODO: 每篇文章对应一个分析器和下载器和导出器；

    /// <summary>
    /// 抽象处理类
    /// </summary>
    public abstract class Processer : IDisposable
    {
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
        }

    }
}
