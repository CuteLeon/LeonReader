using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeonReader.AbstractSADE
{
    /// <summary>
    /// 文章内容下载器
    /// </summary>
    public abstract class Downloader : Processer
    {
        public Downloader() : base() { }
    }
}
