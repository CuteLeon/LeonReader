using System;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeonReader.Common.Tests
{
    [TestClass()]
    public class LogUtilsTests
    {
        [TestMethod()]
        public void LogFilePathTest()
        {
            Console.WriteLine(LogUtils.LogFilePath);
            Assert.IsTrue(IOUtils.FileExists(LogUtils.LogFilePath));
        }

        [TestMethod()]
        public void WriteLogTest()
        {
            //测试不同日志过滤等级对日志输出的影响
            foreach (LogUtils.LogTypes type in Enum.GetValues(typeof(LogUtils.LogTypes)))
            {
                LogUtils.LogLevel = LogUtils.LogTypes.DEBUG;
                LogUtils.Debug("——————————————");
                LogUtils.Debug("日志过滤等级：{0}", type);

                LogUtils.LogLevel = type;
                LogUtils.WriteLog("测试写入日志。", LogUtils.LogTypes.DEBUG);
                LogUtils.WriteLog("测试写入日志。", LogUtils.LogTypes.INFO);
                LogUtils.WriteLog("测试写入日志。", LogUtils.LogTypes.WARN);
                LogUtils.WriteLog("测试写入日志。", LogUtils.LogTypes.ERROR);
                LogUtils.WriteLog("测试写入日志。", LogUtils.LogTypes.FATAL);
            }
            LogUtils.CloseLogListener();
        }

        [TestMethod]
        public void ParallelTest()
        {
            LogUtils.Debug("串行：");
            for (int i = 0; i < 1000; i++)
                LogUtils.Debug(i.ToString());

            LogUtils.Error("并行：");
            Parallel.For(0, 1000, new Action<int>(x => {
                LogUtils.Fatal(x.ToString());
            }));
        }
    }
}