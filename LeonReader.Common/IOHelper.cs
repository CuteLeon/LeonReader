using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeonReader.Common
{
    /// <summary>
    /// IO助手
    /// </summary>
    public static class IOHelper
    {
        /// <summary>
        /// 安全的合并链接
        /// </summary>
        /// <param name="WebSite"></param>
        /// <param name="Router"></param>
        /// <returns></returns>
        public static string LinkCombine(string WebSite, string Router)
        {
            if (WebSite.EndsWith("/"))
                return WebSite + (Router.StartsWith("/") ? Router.Substring(1) : Router);
            else
                return WebSite + (Router.StartsWith("/") ? "" : "/") + Router;
        }


        /// <summary>
        /// 安全的合并路径
        /// </summary>
        /// <param name="DirectoryPath"></param>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static string PathCombine(string DirectoryPath, string FileName) => Path.Combine(DirectoryPath, FileName);

        /// <summary>
        /// 返回指定路径字符串的文件名和扩展名
        /// </summary>
        /// <param name="FilePath">文件路径</param>
        /// <returns></returns>
        public static string GetFileName(string FilePath) => Path.GetFileName(FilePath);

        //TODO: 无占用地读取图像文件 function

    }
}
