using Microsoft.VisualStudio.TestTools.UnitTesting;
using NISOCountries.GeoNames;
using System.Linq;

namespace NISOCountries.Tests
{
    [TestClass]
    public class GeonamesTests
    {
        [TestMethod]
        public void GeonamesISOCountryReader_ParsesFile_Correctly()
        {
            var r = TestUtil.GetTestFileReader();

            var target = new GeonamesISOCountryReader(r)
                .Parse(@"Test\fixtures\geonames_testfile.txt")
                .ToArray();

            Assert.AreEqual(12, target.Length);
            Assert.AreEqual("NL", target[5].Alpha2);
            Assert.AreEqual("NLD", target[5].Alpha3);
            Assert.AreEqual("528", target[5].Numeric);
            Assert.AreEqual("Netherlands", target[5].CountryName);
            Assert.AreEqual("NL", target[5].FIPS);
        }
    }
}
