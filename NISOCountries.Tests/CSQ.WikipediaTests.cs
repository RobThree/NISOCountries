using Microsoft.VisualStudio.TestTools.UnitTesting;
using NISOCountries.Wikipedia.CSQ;
using System.Linq;

namespace NISOCountries.Tests
{
    [TestClass]
    public class CSQWikipediaTests
    {
        [TestMethod]
        public void CSQ_WikipediaISOCountryReader_ParsesFile_Correctly()
        {
            var r = TestUtil.GetTestFileReader();

            var target = new WikipediaISOCountryReader(r)
                .Parse(@"Test\fixtures\wikipedia_testfile.htm")
                .ToArray();

            Assert.AreEqual(11, target.Length);
            Assert.AreEqual("NL", target[5].Alpha2);
            Assert.AreEqual("NLD", target[5].Alpha3);
            Assert.AreEqual("528", target[5].Numeric);
            Assert.AreEqual("NETHERLANDS", target[5].CountryName);
        }
    }
}
