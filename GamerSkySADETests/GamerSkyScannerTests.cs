using Microsoft.VisualStudio.TestTools.UnitTesting;
using GamerSkySADE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LeonReader.AbstractSADE;

namespace GamerSkySADE.Tests
{
    [TestClass()]
    public class GamerSkyScannerTests
    {
        [TestMethod()]
        public void ProcessTest()
        {
            Scanner scanner = new GamerSkyScanner();
            scanner.Process();
        }
    }
}