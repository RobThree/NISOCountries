using NISOCountries.Core;
using NISOCountries.Core.SourceProviders;
using NISOCountries.GeoNames;
using NISOCountries.Ripe;
using NISOCountries.Wikipedia;
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
            var w = new WikipediaReader(s).Parse(@"Testdata\wikipedia_testfile.htm");
            var r = new RipeReader(s).Parse(@"Testdata\ripe_testfile.txt");
            var g = new GeonamesReader(s).Parse(@"Testdata\geonames_testfile.txt");

            var q = r.Cast<IISORecord>()
                .Union(w.Cast<IISORecord>())
                .Union(g.Cast<IISORecord>())
                .ToArray();
        }

        //TODO: Unittests for readers, valuenormalizers, sourceproviders
    }
}
