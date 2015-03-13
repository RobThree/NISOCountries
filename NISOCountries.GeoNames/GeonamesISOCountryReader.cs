using NISOCountries.Core;
using NISOCountries.Core.SourceProviders;

namespace NISOCountries.GeoNames
{
    public class GeonamesISOCountryReader : ISOCountryReader<GeonamesCountry>
    {
        public GeonamesISOCountryReader()
            : base(new GeonamesParser(), new GeonamesNormalizer()) { }

        public GeonamesISOCountryReader(ISourceProvider sourceProvider)
            : base(new GeonamesParser(), new GeonamesNormalizer(), sourceProvider) { }

    }
}
