using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LeonReader.Common
{
    /// <summary>
    /// 网络助手
    /// </summary>
    public static class NetHelper
    {
        /// <summary>
        /// 获取网页内容
        /// </summary>
        /// <param name="address">网页地址</param>
        /// <returns></returns>
        public static string GetWebPage(Uri address)
        {
            try
            {
                return GetWebPage(address.AbsoluteUri);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取网页内容
        /// </summary>
        /// <param name="address">网页地址</param>
        /// <returns></returns>
        public static string GetWebPage(string address)
        {
            using (WebClient client = new WebClient()
            {
                BaseAddress = address,
                Encoding = Encoding.UTF8
            })
            {
                client.Headers.Add(HttpRequestHeader.Accept, "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
                client.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36");

                try
                {
                    return client.DownloadString(address);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
