using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractADE;
using LeonReader.Model;
using LeonReader.Common;

namespace GamerSkyADE
{
    public class GamerSkyScanner : Scanner
    {
        /// <summary>
        /// 目标地址
        /// </summary>
        public new Uri TargetURI = new Uri(@"https://www.gamersky.com/ent/qw/");

        /// <summary>
        /// 扫描 GamerSky 文章
        /// </summary>
        public override void Process()
        {
            Console.WriteLine("GamerSky - Scanner.");
            if (String.IsNullOrEmpty(TargetURI?.AbsoluteUri)) throw new Exception("目标地址为空");

            Console.WriteLine($"开始扫描目录：{TargetURI.ToString()}");
            string CatalogContent = string.Empty;
            try
            {
                CatalogContent = NetHelper.GetWebPage(TargetURI);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (string.IsNullOrEmpty(CatalogContent))
            {
                throw new Exception("获取目录地址内容为空");
            }

            //Console.WriteLine(CatalogContent);
        }

    }
}
