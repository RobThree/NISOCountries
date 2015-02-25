using NISOCountries.Core;
using NISOCountries.Core.SourceProviders;
using NISOCountries.Core.ValueNormalizers;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NISOCountries.Ripe
{
    public class RipeReader : BaseReader<RipeRecord>
    {
        public const string DEFAULTURL = @"ftp://ftp.ripe.net/iso3166-countrycodes.txt";

        private static Regex _countryregex = new Regex(@"^(?<name>(.*?\s)?.*?)\s+(?<iso2>[A-Z]{2})\s+(?<iso3>[A-Z]{3})\s+(?<numeric>[0-9]{3})\s*?", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.ExplicitCapture);

         public RipeReader()
            : this(new RipeNormalizer()) { }

         public RipeReader(IValueNormalizer<RipeRecord> valueNormalizer)
            : this(valueNormalizer, new CachingWebSource())
        { }

        public RipeReader(ISourceProvider sourceProvider)
             : this(new RipeNormalizer(), sourceProvider)
        { }

        public RipeReader(IValueNormalizer<RipeRecord> valueNormalizer, ISourceProvider sourceProvider)
            : base(valueNormalizer, sourceProvider)
        { }

        public override IEnumerable<RipeRecord> Parse(string source)
        {
            using (var s = this.SourceProvider.GetStreamReader(source))
            {
                return _countryregex.Matches(s.ReadToEnd())
                    .Cast<Match>()
                    .Where(m => m.Success)
                    .Select(m => new RipeRecord
                    {
                        CountryName = m.Groups["name"].Value.Replace('\n', ' '),
                        Alpha2 = m.Groups["iso2"].Value,
                        Alpha3 = m.Groups["iso3"].Value,
                        Numeric = m.Groups["numeric"].Value,
                    })
                    .Select(v => this.ValueNormalizer.Normalize(v));
            }
        }
    }
}
