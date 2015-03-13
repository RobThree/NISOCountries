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

            Assert.AreEqual(12, target.Length);
            Assert.AreEqual("NL", target[5].Alpha2);
            Assert.AreEqual("NLD", target[5].Alpha3);
            Assert.AreEqual("528", target[5].Numeric);
            Assert.AreEqual("NETHERLANDS", target[5].CountryName);

            //RIPE has, currently, a newline in the middle of this countryname (and a * at the end). Like this:
            //UNITED KINGDOM OF GREAT BRITAIN AND NORTHERN
            //IRELAND*	 				GB 	GBR 	826
            //Make sure this is "normalized out"
            Assert.AreEqual("UNITED KINGDOM OF GREAT BRITAIN AND NORTHERN IRELAND", target[10].CountryName);
        }
    }
}
