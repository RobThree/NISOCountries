using Microsoft.VisualStudio.TestTools.UnitTesting;
using NISOCountries.Datahub;
using System.Linq;

namespace NISOCountries.Tests
{
    [TestClass]
    public class DatahubTests
    {
        [TestMethod]
        public void DatahubISOCountryReader_ParsesFile_Correctly()
        {
            var r = TestUtil.GetTestFileReader();

            var target = new DatahubISOCountryReader(r)
                .Parse(@"Test\fixtures\datahub_testfile.txt")
                .ToArray();

            Assert.AreEqual(10, target.Length);
            Assert.AreEqual("NL", target[9].Alpha2);
            Assert.AreEqual("NLD", target[9].Alpha3);
            Assert.AreEqual("528", target[9].Numeric);
            Assert.AreEqual("Netherlands", target[9].CountryName);
        }
    }
}
