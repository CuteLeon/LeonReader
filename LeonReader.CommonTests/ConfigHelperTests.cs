using Microsoft.VisualStudio.TestTools.UnitTesting;
using LeonReader.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeonReader.Common.Tests
{
    [TestClass()]
    public class ConfigHelperTests
    {
        [TestMethod()]
        public void SingltonConfigHelperTest()
        {
            ConfigHelper configHelper_0 = ConfigHelper.GetConfigHelper;
            ConfigHelper configHelper_1 = ConfigHelper.GetConfigHelper;

            Assert.AreSame(configHelper_0, configHelper_1);
        }
    }
}