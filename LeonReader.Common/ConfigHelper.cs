using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeonReader.Common
{
    /// <summary>
    /// 配置助手（饿汉单实例模式）
    /// </summary>
    public sealed class ConfigHelper
    {
        /// <summary>
        /// 获取单实例配置助手
        /// </summary>
        public static ConfigHelper GetConfigHelper { get; } = new ConfigHelper();

        private ConfigHelper() { }

        /// <summary>
        /// 下载目录
        /// </summary>
        public string DownloadDirectory { get; } = IOHelper.PathCombine(Environment.CurrentDirectory, "Articles");
    }
}
