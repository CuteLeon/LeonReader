using Microsoft.VisualStudio.TestTools.UnitTesting;
using LeonReader.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LeonReader.Common.Tests
{
    [TestClass()]
    public class LogHelperTests
    {
        [TestMethod()]
        public void LogFilePathTest()
        {
            Console.WriteLine(LogHelper.LogFilePath);
            Assert.IsTrue(IOHelper.FileExists(LogHelper.LogFilePath));
        }

        [TestMethod()]
        public void WriteLogTest()
        {
            //测试不同日志过滤等级对日志输出的影响
            foreach (LogHelper.LogTypes type in Enum.GetValues(typeof(LogHelper.LogTypes)))
            {
                LogHelper.LogLevel = LogHelper.LogTypes.DEBUG;
                LogHelper.Debug("——————————————");
                LogHelper.Debug("日志过滤等级：{0}", type);

                LogHelper.LogLevel = type;
                LogHelper.WriteLog("测试写入日志。", LogHelper.LogTypes.DEBUG);
                LogHelper.WriteLog("测试写入日志。", LogHelper.LogTypes.INFO);
                LogHelper.WriteLog("测试写入日志。", LogHelper.LogTypes.WARN);
                LogHelper.WriteLog("测试写入日志。", LogHelper.LogTypes.ERROR);
                LogHelper.WriteLog("测试写入日志。", LogHelper.LogTypes.FATAL);
            }
            LogHelper.CloseLogListener();
        }
    }
}