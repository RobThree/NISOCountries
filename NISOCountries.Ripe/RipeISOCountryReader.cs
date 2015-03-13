using NISOCountries.Core;
using NISOCountries.Core.SourceProviders;

namespace NISOCountries.Ripe
{
    public class RipeISOCountryReader : ISOCountryReader<RipeCountry>
    {
        public RipeISOCountryReader()
            : base(new RipeParser(), new RipeNormalizer()) { }

        public RipeISOCountryReader(ISourceProvider sourceProvider)
            : base(new RipeParser(), new RipeNormalizer(), sourceProvider) { }

    }
}
