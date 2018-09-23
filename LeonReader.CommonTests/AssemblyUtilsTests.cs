using System;
using System.Linq;
using System.Reflection;

using GamerSkySADE;

using LeonReader.AbstractSADE;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeonReader.Common.Tests
{
    [TestClass()]
    public class AssemblyUtilsTests
    {

        [TestMethod()]
        public void CreateAssemblyTest()
        {
            Assert.IsNotNull(AssemblyUtils.CreateAssembly("LeonReader.Common.dll"));
            Assert.ThrowsException<System.IO.FileNotFoundException>(() => AssemblyUtils.CreateAssembly("LeonReader.xxx.dll"));
        }

        [TestMethod()]
        public void GetSubTypesTest()
        {
            Assembly assembly = AssemblyUtils.CreateAssembly("GamerSkySADE.dll");
            Assert.IsTrue(assembly.GetSubTypes(typeof(Scanner)).Select(type => type.FullName).ToArray().Contains(typeof(GamerSkyScanner).FullName));
            Assert.IsFalse(assembly.GetSubTypes(typeof(Analyzer)).Select(type => type.FullName).ToArray().Contains(typeof(GamerSkyScanner).FullName));
        }

        [TestMethod()]
        public void CreateInstanceTest()
        {
            Assembly assembly = AssemblyUtils.CreateAssembly("GamerSkySADE.dll");
            Type ScannerType = assembly.GetSubTypes(typeof(Scanner)).FirstOrDefault();
            Assert.IsNotNull(ScannerType);
            Scanner scanner = assembly.CreateInstance(ScannerType) as Scanner;
            Assert.IsInstanceOfType(scanner, ScannerType);
            Assert.IsNotInstanceOfType(scanner, typeof(Analyzer));
        }
    }
}