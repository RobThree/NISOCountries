using NISOCountries.Core;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NISOCountries.GeoNames
{
    public class GeonamesParser : IStreamParser<GeonamesRecord>
    {
        public const string DEFAULTURL = @"http://download.geonames.org/export/dump/countryInfo.txt";

        public IEnumerable<GeonamesRecord> Parse(StreamReader streamReader)
        {
            return streamReader.ReadAllLines()
                .Where(l => !l.StartsWith("#"))
                .Select(l => l.Split('\t'))
                .Where(v => v.Length >= 5)
                .Select(v => new GeonamesRecord
                {
                    Alpha2 = v[0],
                    Alpha3 = v[1],
                    Numeric = v[2],
                    Fips = v[3],
                    CountryName = v[4]
                });
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
