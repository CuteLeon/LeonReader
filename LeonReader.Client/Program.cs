using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using LeonReader.Common;

namespace LeonReader.Client
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            ConfigLogHelper();
            LogHelper.Fatal("系统启动");
            LogHelper.Fatal("———————————");

            LogHelper.LogLevel = LogHelper.LogTypes.DEBUG;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());

            LogHelper.Fatal("———————————");
            LogHelper.Fatal("系统关闭");
            LogHelper.CloseLogListener();
        }

        /// <summary>
        /// 配置日志助手
        /// </summary>
        static void ConfigLogHelper()
        {
#if DEBUG
            LogHelper.LogLevel = LogHelper.LogTypes.DEBUG;
            LogHelper.Info("调试模式");
#else
            LogHelper.LogLevel = LogHelper.LogTypes.INFO;
            LogHelper.Info("非调试模式");
#endif
        }

    }
}
