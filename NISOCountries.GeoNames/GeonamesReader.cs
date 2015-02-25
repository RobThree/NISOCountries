using NISOCountries.Core;
using NISOCountries.Core.SourceProviders;
using NISOCountries.Core.ValueNormalizers;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NISOCountries.GeoNames
{
    public class GeonamesReader : BaseReader<GeonamesRecord>
    {
        public const string DEFAULTURL = @"http://download.geonames.org/export/dump/countryInfo.txt";

        public GeonamesReader()
            : this(new GeonamesNormalizer()) { }

        public GeonamesReader(IValueNormalizer<GeonamesRecord> valueNormalizer)
            : this(valueNormalizer, new CachingWebSource())
        { }

        public GeonamesReader(ISourceProvider sourceProvider)
            : this(new GeonamesNormalizer(), sourceProvider)
        { }

        public GeonamesReader(IValueNormalizer<GeonamesRecord> valueNormalizer, ISourceProvider sourceProvider)
            : base(valueNormalizer, sourceProvider)
        { }

        public override IEnumerable<GeonamesRecord> Parse(string source)
        {
            using (var s = this.SourceProvider.GetStreamReader(source))
            {
                return s.ReadAllLines()
                    .Where(l => !l.StartsWith("#"))
                    .Select(l => l.Split('\t'))
                    .Where(v => v.Length >= 5)
                    .Select(v => new GeonamesRecord
                    {
                        Alpha2 = v[0],
                        Alpha3 = v[1],
                        Numeric = v[2],
                        Fips = v[3],
                        EnglishName = v[4]
                    });
            }
        }
    }

    internal static class StreamReaderExt
    {
        public static IEnumerable<string> ReadAllLines(this StreamReader sr)
        {
            var lines = new List<string>(300);  //Make an ample guess; currently there are about ~250 ISO country codes
            string line;
            while ((line = sr.ReadLine()) != null)
                lines.Add(line);
            return lines;
        }
    }
}
