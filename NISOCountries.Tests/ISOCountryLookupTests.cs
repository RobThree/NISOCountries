using Microsoft.VisualStudio.TestTools.UnitTesting;
using NISOCountries.Core;

namespace NISOCountries.Tests
{
    [TestClass]
    public class ISOCountryLookupTests
    {
        [TestMethod]
        public void ISOCountryLookup_AutoDetectCode_WorksForAllCombinations()
        {
            var target = new ISOCountryLookup<TestCountry>(new TestCountryReader(), null);

            TestCountry result;
            Assert.IsTrue(target.TryGet("TC", out result));
            Assert.AreEqual("TestCountry", result.CountryName);

            Assert.IsTrue(target.TryGet("TST", out result));
            Assert.AreEqual("TestCountry", result.CountryName);

            Assert.IsTrue(target.TryGet("001", out result));
            Assert.AreEqual("TestCountry", result.CountryName);

            Assert.IsFalse(target.TryGet("XX", out result));
            Assert.IsFalse(target.TryGet("XXX", out result));
            Assert.IsFalse(target.TryGet("999", out result));
        }

        [TestMethod]
        public void ISOCountryLookup_AutoDetectCode_IsNotCaseSensitive()
        {
            var target = new ISOCountryLookup<TestCountry>(new TestCountryReader(), null,true);

            TestCountry result;
            Assert.IsTrue(target.TryGet("TC", out result));
            Assert.IsTrue(target.TryGet("tc", out result));
            Assert.IsTrue(target.TryGet("Tc", out result));

            Assert.IsTrue(target.TryGet("TST", out result));
            Assert.IsTrue(target.TryGet("tst", out result));
            Assert.IsTrue(target.TryGet("TsT", out result));
        }

        [TestMethod]
        public void ISOCountryLookup_AutoDetectCode_IsCaseSensitive()
        {
            var target = new ISOCountryLookup<TestCountry>(new TestCountryReader(), null, false);

            TestCountry result;
            Assert.IsTrue(target.TryGet("TC", out result));
            Assert.IsFalse(target.TryGet("tc", out result));
            Assert.IsFalse(target.TryGet("Tc", out result));

            Assert.IsTrue(target.TryGet("TST", out result));
            Assert.IsFalse(target.TryGet("tst", out result));
            Assert.IsFalse(target.TryGet("TsT", out result));
        }
    }
}
