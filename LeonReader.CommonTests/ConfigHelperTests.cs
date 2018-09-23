using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeonReader.Common.Tests
{
    [TestClass()]
    public class ConfigHelperTests
    {
        [TestMethod()]
        public void SingltonConfigHelperTest()
        {
            ConfigHelper ConfigHelper_0 = ConfigHelper.GetConfigHelper;
            ConfigHelper ConfigHelper_1 = ConfigHelper.GetConfigHelper;

            Assert.AreSame(ConfigHelper_0, ConfigHelper_1);
        }
    }
}