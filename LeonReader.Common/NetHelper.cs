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
        /// 安全的合并链接
        /// </summary>
        /// <param name="WebSite"></param>
        /// <param name="Router"></param>
        /// <returns></returns>
        public static string LinkCombine(string WebSite, string Router)
        {
            if (string.IsNullOrEmpty(WebSite)) throw new Exception("合并链接时遇到错误，网站地址为空字符串");
            if (string.IsNullOrEmpty(Router)) return WebSite;

            if (WebSite.EndsWith("/"))
                return WebSite + (Router.StartsWith("/") ? Router.Substring(1) : Router);
            else
                return WebSite + (Router.StartsWith("/") ? "" : "/") + Router;
        }

        /// <summary>
        /// 安全的合并链接
        /// </summary>
        /// <param name="WebSite"></param>
        /// <param name="Router"></param>
        /// <returns></returns>
        public static string LinkCombine(Uri WebSite, string Router)
        {
            if (WebSite == null) throw new Exception("合并链接时遇到错误，网站地址为空对象");
            if (!WebSite.IsAbsoluteUri) throw new Exception("合并链接时遇到错误，网站地址不是绝对地址");

            return $"{WebSite.GetLeftPart(UriPartial.Authority)}{(Router.StartsWith("/") ? "" : "/")}{Router}";
        }

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
            LogHelper.Debug($"获取网页内容：{address}");
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
                    LogHelper.Error($"获取网页内容遇到异常：{address}，{ex.Message}");
                    throw ex;
                }
            }
        }
    }
}
