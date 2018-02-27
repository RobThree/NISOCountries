using NISOCountries.Core;
using NISOCountries.Core.SourceProviders;
using System.Collections.Generic;

namespace NISOCountries.Ripe
{
    public class RipeISOCountryReader : ISOCountryReader<RipeCountry>
    {
        public const string DEFAULTURL = @"ftp://ftp.ripe.net/iso3166-countrycodes.txt";

        public RipeISOCountryReader()
            : base(new RipeParser(), new RipeNormalizer()) { }

        public RipeISOCountryReader(ISourceProvider sourceProvider)
            : base(new RipeParser(), new RipeNormalizer(), sourceProvider) { }

        public override IEnumerable<RipeCountry> GetDefault()
        {
            return Parse(DEFAULTURL);

        }
    }
}
