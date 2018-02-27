using NISOCountries.Core;
using NISOCountries.Core.SourceProviders;
using System.Collections.Generic;

namespace NISOCountries.GeoNames
{
    public class GeonamesISOCountryReader : ISOCountryReader<GeonamesCountry>
    {
        public const string DEFAULTURL = @"http://download.geonames.org/export/dump/countryInfo.txt";

        public GeonamesISOCountryReader()
            : base(new GeonamesParser(), new GeonamesNormalizer()) { }

        public GeonamesISOCountryReader(ISourceProvider sourceProvider)
            : base(new GeonamesParser(), new GeonamesNormalizer(), sourceProvider) { }

        public override IEnumerable<GeonamesCountry> GetDefault()
        {
            return Parse(DEFAULTURL);
        }
    }
}
