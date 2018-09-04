using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
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
            //注册系统事件
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.ThreadException += Application_ThreadException;
            Application.ApplicationExit += Application_ApplicationExit;

            //配置依赖组件
            ConfigLogHelper();
            LogHelper.Fatal("系统启动");
            LogHelper.Fatal("———————————");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            LogHelper.Fatal("———————————");
            LogHelper.Fatal("系统关闭");
            LogHelper.CloseLogListener();
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception UnhandledException = e.ExceptionObject as Exception;
            string ExceptionDescription = string.Format(
                "应用域内发现未被捕获的异常：\r\n" +
                "   异常类型 : {0}\r\n" +
                "   异常地址 : {1}\r\n" +
                "   出错方法 : {2}\r\n" +
                "   所在文件 : {3}\r\n" +
                "   异常信息 : {4}\r\n" +
                "   调用堆栈 : \r\n{5}\r\n" +
                "   即将终止 : {6}\r\n" +
                "   ——————————\r\n" +
                "   日志文件：{7}\r\n" +
                "   出错方法MSIL : {8}",
                UnhandledException.GetType().ToString(),
                UnhandledException.Source,
                UnhandledException.TargetSite.Name,
                UnhandledException.TargetSite.Module.FullyQualifiedName,
                UnhandledException.Message,
                UnhandledException.StackTrace,
                e.IsTerminating,
                LogHelper.LogFilePath,
                string.Join("", UnhandledException.TargetSite.GetMethodBody().GetILAsByteArray())
            );

            LogHelper.Fatal(ExceptionDescription);

            if( MessageBox.Show(ExceptionDescription, "点击<确定>打开日志文件", MessageBoxButtons.OKCancel)== DialogResult.OK)
                Process.Start(LogHelper.LogFilePath);
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Exception UnhandledException = e.Exception as Exception;
            string ExceptionDescription = string.Format(
                "发生未捕获线程异常：\r\n" +
                "   异常类型 : {0}\r\n" +
                "   异常地址 : {1}\r\n" +
                "   出错方法 : {2}\r\n" +
                "   所在文件 : {3}\r\n" +
                "   异常信息 : {4}\r\n" +
                "   调用堆栈 : \r\n{5}\r\n" +
                "   ——————————\r\n" +
                "   日志文件：{6}\r\n" +
                "   出错方法MSIL : {7}",
                UnhandledException.GetType().ToString(),
                UnhandledException.Source,
                UnhandledException.TargetSite.Name,
                UnhandledException.TargetSite.Module.FullyQualifiedName,
                UnhandledException.Message,
                UnhandledException.StackTrace,
                LogHelper.LogFilePath,
                string.Join("", UnhandledException.TargetSite.GetMethodBody().GetILAsByteArray())
            );

            LogHelper.Fatal(ExceptionDescription);

            if (MessageBox.Show(ExceptionDescription, "点击<确定>打开日志文件", MessageBoxButtons.OKCancel) == DialogResult.OK)
                Process.Start(LogHelper.LogFilePath);
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
