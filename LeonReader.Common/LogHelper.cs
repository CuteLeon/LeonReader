﻿using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeonReader.Common
{
    /// <summary>
    /// 日志助手（饿汉单实例模式）
    /// </summary>
    public static class LogHelper
    {
        /// <summary>
        /// 日志类型枚举
        /// </summary>
        public enum LogTypes
        {
            /// <summary>
            /// 调试
            /// </summary>
            DEBUG = 0,
            /// <summary>
            /// 信息
            /// </summary>
            INFO = 1,
            /// <summary>
            /// 警告
            /// </summary>
            WARN = 2,
            /// <summary>
            /// 错误
            /// </summary>
            ERROR = 3,
            /// <summary>
            /// 致命
            /// </summary>
            FATAL = 4
        }

        private static LogTypes logLevel = LogTypes.DEBUG;
        /// <summary>
        /// 日志监听级别（仅输出级别大于或等于当前级别的日志）
        /// </summary>
        public static LogTypes LogLevel { get => logLevel; set => logLevel = value; }

        /// <summary>
        /// 获取日志文件路径
        /// </summary>
        /// <returns></returns>
        public static string LogFilePath { get => LogListener?.FullLogFileName ?? "<未创建日志监听器>"; }

        /// <summary>
        /// 日志监听器
        /// </summary>
        private static FileLogTraceListener LogListener { get; }=new FileLogTraceListener("LeonReaderLogHelper")
        {
            DiskSpaceExhaustedBehavior = DiskSpaceExhaustedOption.DiscardMessages,
            LogFileCreationSchedule = LogFileCreationScheduleOption.None,
            Location = LogFileLocation.Custom,
            MaxFileSize = 100 * 1024 * 1024,
            CustomLocation = IOHelper.PathCombine(Environment.CurrentDirectory,"Logs"),
            BaseFileName = $"Log_{DateTime.Now.ToString("yyyyMMdd_hh_mm_ss.fff")}",
            Encoding = Encoding.UTF8,
            IncludeHostName = true,
            AutoFlush = true,
            Append = true,
            IndentSize = 4,
        };

        /// <summary>
        /// 写入一行日志
        /// </summary>
        /// <param name="LogMessage">日志消息</param>
        /// <param name="LogType">日志类型</param>
        /// <param name="LogValues">参数值</param>
        public static void WriteLog(string LogMessage, LogTypes LogType = LogTypes.INFO, params object[] LogValues) =>
            WriteLog(string.Format(LogMessage, LogValues), LogType);

        /// <summary>
        /// 写入一行日志
        /// </summary>
        /// <param name="LogMessage">日志消息</param>
        /// <param name="LogType">日志类型</param>
        public static void WriteLog(string LogMessage, LogTypes LogType = LogTypes.INFO)
        {
            //对低等级的日志进行拦截
            if (LogType < LogLevel) return;

            string Message = string.Format(
                @"\{0}/   <{1}>   {2}",
                DateTime.Now.ToString("yyyy/MM/dd-hh:mm:ss.fff"),
                LogType.ToString().PadRight(5),
                LogMessage
                );
            LogListener?.WriteLine(Message);

            Console.WriteLine(Message);
        }

        /// <summary>
        /// 写入一条调试日志
        /// </summary>
        /// <param name="LogMessage">日志信息</param>
        /// <param name="LogValues">参数值</param>
        public static void Debug(string LogMessage, params object[] LogValues) => WriteLog(LogMessage, LogTypes.DEBUG, LogValues);
        /// <summary>
        /// 写入一条调试日志
        /// </summary>
        /// <param name="LogMessage">日志信息</param>
        public static void Debug(string LogMessage) => WriteLog(LogMessage, LogTypes.DEBUG);

        /// <summary>
        /// 写入一条信息日志
        /// </summary>
        /// <param name="LogMessage">日志信息</param>
        /// <param name="LogValues">参数值</param>
        public static void Info(string LogMessage, params object[] LogValues) => WriteLog(LogMessage, LogTypes.INFO, LogValues);
        /// <summary>
        /// 写入一条信息日志
        /// </summary>
        /// <param name="LogMessage">日志信息</param>
        public static void Info(string LogMessage) => WriteLog(LogMessage, LogTypes.INFO);

        /// <summary>
        /// 写入一条警告日志
        /// </summary>
        /// <param name="LogMessage">日志信息</param>
        /// <param name="LogValues">参数值</param>
        public static void Warn(string LogMessage, params object[] LogValues) => WriteLog(LogMessage, LogTypes.WARN, LogValues);
        /// <summary>
        /// 写入一条警告日志
        /// </summary>
        /// <param name="LogMessage">日志信息</param>
        public static void Warn(string LogMessage) => WriteLog(LogMessage, LogTypes.WARN);

        /// <summary>
        /// 写入一条错误日志
        /// </summary>
        /// <param name="LogMessage">日志信息</param>
        /// <param name="LogValues">参数值</param>
        public static void Error(string LogMessage, params object[] LogValues) => WriteLog(LogMessage, LogTypes.ERROR, LogValues);
        /// <summary>
        /// 写入一条错误日志
        /// </summary>
        /// <param name="LogMessage">日志信息</param>
        public static void Error(string LogMessage) => WriteLog(LogMessage, LogTypes.ERROR);

        /// <summary>
        /// 写入一条致命日志
        /// </summary>
        /// <param name="LogMessage">日志信息</param>
        /// <param name="LogValues">参数值</param>
        public static void Fatal(string LogMessage, params object[] LogValues) => WriteLog(LogMessage, LogTypes.FATAL, LogValues);
        /// <summary>
        /// 写入一条致命日志
        /// </summary>
        /// <param name="LogMessage">日志信息</param>
        public static void Fatal(string LogMessage) => WriteLog(LogMessage, LogTypes.FATAL);

        /// <summary>
        /// 关闭并释放日志监听器
        /// </summary>
        public static void CloseLogListener()
        {
            Info("关闭日志记录器 ...");
            LogListener?.Flush();
            LogListener?.Close();
            LogListener?.Dispose();
        }

    }
}
