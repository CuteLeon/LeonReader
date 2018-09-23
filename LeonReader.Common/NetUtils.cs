using System;
using System.Net;
using System.Text;

namespace LeonReader.Common
{
    /// <summary>
    /// 网络助手
    /// </summary>
    public static class NetUtils
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
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取网页内容
        /// </summary>
        /// <param name="address">网页地址</param>
        /// <returns></returns>
        public static string GetWebPage(string address)
        {
            LogUtils.Debug($"获取网页内容：{address}");
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
                    LogUtils.Error($"获取网页内容遇到异常：{address}，{ex.Message}");
                    throw;
                }
            }
        }

        /// <summary>
        /// 下载网络文件
        /// </summary>
        /// <param name="address">网络地址</param>
        /// <param name="filePath">文件路径</param>
        public static void DownloadWebFile(Uri uri, string filePath)
        {
            DownloadWebFile(uri.AbsoluteUri, filePath);
        }

        /// <summary>
        /// 下载网络文件
        /// </summary>
        /// <param name="address">网络地址</param>
        /// <param name="filePath">文件路径</param>
        public static void DownloadWebFile(string address,string filePath)
        {
            LogUtils.Debug($"下载网络文件：{address} => {filePath}");
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
                    client.DownloadFile(address, filePath);
                }
                catch (Exception ex)
                {
                    LogUtils.Error($"下载网络文件遇到异常：{address}，{ex.Message}");
                    throw;
                }
            }
        }

    }
}
