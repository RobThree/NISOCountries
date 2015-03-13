using NISOCountries.Core;
using NISOCountries.Core.SourceProviders;
using System.Collections.Generic;

namespace NISOCountries.Ripe
{
    public class RipeISOCountryReader : ISOCountryReader<RipeCountry>
    {
        public RipeISOCountryReader()
            : base(new RipeParser(), new RipeNormalizer()) { }

        public RipeISOCountryReader(ISourceProvider sourceProvider)
            : base(new RipeParser(), new RipeNormalizer(), sourceProvider) { }

        public override IEnumerable<RipeCountry> GetDefault()
        {
            return this.Parse(@"ftp://ftp.ripe.net/iso3166-countrycodes.txt");

        }
    }
}
