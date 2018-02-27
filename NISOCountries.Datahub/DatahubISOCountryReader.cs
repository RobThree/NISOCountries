using NISOCountries.Core;
using NISOCountries.Core.SourceProviders;
using System.Collections.Generic;

namespace NISOCountries.Datahub
{

    public class DatahubISOCountryReader : ISOCountryReader<DatahubCountry>
    {
        public DatahubISOCountryReader()
            : base(new DatahubParser(), new DatahubNormalizer()) { }

        public DatahubISOCountryReader(ISourceProvider sourceProvider)
            : base(new DatahubParser(), new DatahubNormalizer(), sourceProvider) { }

        public override IEnumerable<DatahubCountry> GetDefault()
        {
            return Parse(@"https://datahub.io/core/country-codes/r/country-codes.csv");
        }
    }
}
