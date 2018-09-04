using Microsoft.VisualStudio.TestTools.UnitTesting;
using LeonReader.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using LeonReader.AbstractSADE;
using GamerSkySADE;

namespace LeonReader.Common.Tests
{
    [TestClass()]
    public class AssemblyHelperTests
    {

        [TestMethod()]
        public void CreateAssemblyTest()
        {
            Assert.IsNotNull(AssemblyHelper.CreateAssembly("LeonReader.Common.dll"));
            Assert.IsNull(AssemblyHelper.CreateAssembly("LeonReader.xxx.dll"));
        }

        [TestMethod()]
        public void GetSubTypesTest()
        {
            Assembly assembly = AssemblyHelper.CreateAssembly("GamerSkySADE.dll");
            Assert.IsTrue(assembly.GetSubTypes(typeof(Scanner)).Select(type => type.FullName).ToArray().Contains(typeof(GamerSkyScanner).FullName));
            Assert.IsFalse(assembly.GetSubTypes(typeof(Analyzer)).Select(type => type.FullName).ToArray().Contains(typeof(GamerSkyScanner).FullName));
        }

        [TestMethod()]
        public void CreateInstanceTest()
        {
            Assembly assembly = AssemblyHelper.CreateAssembly("GamerSkySADE.dll");
            Type ScannerType = assembly.GetSubTypes(typeof(Scanner)).FirstOrDefault();
            Assert.IsNotNull(ScannerType);
            Scanner scanner = assembly.CreateInstance(ScannerType) as Scanner;
            Assert.IsInstanceOfType(scanner, ScannerType);
            Assert.IsNotInstanceOfType(scanner, typeof(Analyzer));
        }
    }
}