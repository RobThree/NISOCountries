using Microsoft.VisualStudio.TestTools.UnitTesting;
using NISOCountries.Core;
using NISOCountries.Core.SourceProviders;
using NISOCountries.GeoNames;
using NISOCountries.Ripe;
using NISOCountries.Wikipedia;
using System.Linq;
using System.Text;
using csq = NISOCountries.Wikipedia.CSQ;
using hap = NISOCountries.Wikipedia.HAP;

namespace NISOCountries.Tests
{
    [TestClass]
    public class UnitTest1
    {
        ISourceProvider GetTestFileReader(Encoding encoding = null)
        {
            return new FileSource(encoding ?? Encoding.UTF8);
        }

        [TestMethod]
        public void Example()
        {
            var s = GetTestFileReader();

            var w = new ISOCountryReader<GeonamesRecord>(new GeonamesParser(), s).Parse(@"Test\fixtures\geonames_testfile.txt");
            var x = new ISOCountryReader<WikipediaRecord>(new csq.WikipediaParser(), s).Parse(@"Test\fixtures\wikipedia_testfile.htm");
            var y = new ISOCountryReader<WikipediaRecord>(new hap.WikipediaParser(), s).Parse(@"Test\fixtures\wikipedia_testfile.htm");
            var z = new ISOCountryReader<RipeRecord>(new RipeParser(), s).Parse(@"Test\fixtures\ripe_testfile.txt");

            var all = w.Cast<ISORecord>()
                .Union(x)
                .Union(y)
                .Union(z);

            var arr = all.ToArray();

            //var ww = new ISOCountryReader<GeonamesRecord>(new GeonamesParser()).Parse(GeonamesParser.DEFAULTURL);
            //var wx = new ISOCountryReader<WikipediaRecord>(new csq.WikipediaParser()).Parse(csq.WikipediaParser.DEFAULTURL);
            //var wy = new ISOCountryReader<WikipediaRecord>(new hap.WikipediaParser()).Parse(hap.WikipediaParser.DEFAULTURL);
            //var wz = new ISOCountryReader<RipeRecord>(new RipeParser()).Parse(RipeParser.DEFAULTURL);

            //var wall = ww.Cast<ISORecord>()
            //    .Union(wx)
            //    .Union(wy)
            //    .Union(wz);

            //var warr = wall.ToArray();

            var x1 = new ISOCountryLookup<IISORecord>(w);
            var x2 = new ISOCountryLookup<IISORecord>(x);
            var x3 = new ISOCountryLookup<IISORecord>(y);
            var x4 = new ISOCountryLookup<IISORecord>(z);

            var r1 = x1.GetByAlpha2("nl");
            var r2 = x1.GetByAlpha2("NL");

            var r3 = x2.GetByNumeric("634");
            var r4 = x2.GetByNumeric(634);

            var r5 = x3.GetByAlpha3("NLD");

            var q1 = x1.Get("NL");
            var q2 = x1.Get("NLD");
            var q3 = x1.Get("528");

            var w1 = x1["NL"];
            var w2 = x1["NLD"];
            var w3 = x1["528"];

            IISORecord result1;
            IISORecord result2;
            IISORecord result3;
            var q4 = x1.TryGet("NL", out result1);
            var q5 = x1.TryGet("NLD", out result2);
            var q6 = x1.TryGet("528", out result3);

            Assert.ReferenceEquals(r1, r2);
            Assert.ReferenceEquals(r3, r4);
        }

        //TODO: Unittests for readers, valuenormalizers, sourceproviders, ISOCountryLookup etc.
    }
}
