using Microsoft.VisualStudio.TestTools.UnitTesting;
using NISOCountries.Ripe;
using System.Linq;

namespace NISOCountries.Tests
{
    [TestClass]
    public class RipeTests
    {
        [TestMethod]
        public void RipeISOCountryReader_ParsesFile_Correctly()
        {
            var r = TestUtil.GetTestFileReader();

            var target = new RipeISOCountryReader(r)
                .Parse(@"Test\fixtures\ripe_testfile.txt")
                .ToArray();

            Assert.AreEqual(11, target.Length);
            Assert.AreEqual("NL", target[5].Alpha2);
            Assert.AreEqual("NLD", target[5].Alpha3);
            Assert.AreEqual("528", target[5].Numeric);
            Assert.AreEqual("NETHERLANDS", target[5].CountryName);
        }
    }
}
