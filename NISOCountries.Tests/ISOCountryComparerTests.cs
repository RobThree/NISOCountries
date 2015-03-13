using Microsoft.VisualStudio.TestTools.UnitTesting;
using NISOCountries.Core;
using System.Linq;

namespace NISOCountries.Tests
{
    [TestClass]
    public class ISOCountryComparerTests
    {
        [TestMethod]
        public void ISOCountryComparer_Compares_Correctly()
        {
            var l = new TestCountryReader().GetDefault().Cast<ISOCountry>().ToArray();
            var r = new TestCountryReader(new TestLowercasingValueConverter()).GetDefault().Cast<ISOCountry>().ToArray();

            //Make sure we somehow don't have the same reference to the underlying objects
            Assert.IsFalse(object.ReferenceEquals(l[0], r[0]));

            //Check case-insensitity
            var ci = new ISOCountryComparer<ISOCountry>(true);
            Assert.IsFalse(ci.Equals(l[0], l[1]));
            Assert.IsTrue(ci.Equals(l[0], r[0]));

            //Check case-sensitity
            var cs = new ISOCountryComparer<ISOCountry>(false);
            Assert.IsFalse(cs.Equals(l[0], l[1]));
            Assert.IsFalse(cs.Equals(l[0], r[0]));
        }
    }
}
