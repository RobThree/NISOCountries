using NISOCountries.Core;
using NISOCountries.Core.SourceProviders;
using NISOCountries.GeoNames;
using NISOCountries.Ripe;
using NISOCountries.Wikipedia;
using csq = NISOCountries.Wikipedia.CSQ;
using hap = NISOCountries.Wikipedia.HAP;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Text;

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
        public void TestMethod1()
        {
            var s = GetTestFileReader();
            var w = new csq.WikipediaReader(s).Parse(@"Test\fixtures\wikipedia_testfile.htm");
            var h = new hap.WikipediaReader(s).Parse(@"Test\fixtures\wikipedia_testfile.htm");
            var r = new RipeReader(s).Parse(@"Test\fixtures\ripe_testfile.txt");
            var g = new GeonamesReader(s).Parse(@"Test\fixtures\geonames_testfile.txt");

            var qqq = w.OrderBy(c => c.Alpha2).SequenceEqual(h.OrderBy(q => q.Alpha2), new WikipediaComparer());

            //var q = r.Cast<IISORecord>()
            //    .Union(w.Cast<IISORecord>())
            //    .Union(g.Cast<IISORecord>())
            //    .ToArray();

            var x1 = new ISOCountryLookup<IISORecord>(w);
            var x2 = new ISOCountryLookup<IISORecord>(h);
            var x3 = new ISOCountryLookup<IISORecord>(r);
            var x4 = new ISOCountryLookup<IISORecord>(g);

            var x5 = new ISOCountryLookup<WikipediaRecord>(new csq.WikipediaReader(s), @"Test\fixtures\wikipedia_testfile.htm");
            var x6 = new ISOCountryLookup<WikipediaRecord>(new hap.WikipediaReader(s), @"Test\fixtures\wikipedia_testfile.htm");
            var x7 = new ISOCountryLookup<RipeRecord>(new RipeReader(s), @"Test\fixtures\ripe_testfile.txt");
            var x8 = new ISOCountryLookup<GeonamesRecord>(new GeonamesReader(s), @"Test\fixtures\geonames_testfile.txt");

            var r1 = x1.GetByAlpha2("nl");
            var r2 = x1.GetByAlpha2("NL");

            var r3 = x2.GetByNumeric("036");
            var r4 = x2.GetByNumeric(36);

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
