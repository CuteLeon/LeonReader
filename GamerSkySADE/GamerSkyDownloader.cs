using LeonReader.AbstractSADE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSkySADE
{
    public class GamerSkyDownloader : Downloader
    {

        /// <summary>
        /// 内容计数
        /// </summary>
        private int ContentCount = 0;

        /// <summary>
        /// 文章处理源
        /// </summary>
        public override string ASDESource { get; protected set; } = "GamerSky-趣闻";

        protected override void OnProcessStarted(object sender, DoWorkEventArgs e)
        {
            
        }
    }
}
