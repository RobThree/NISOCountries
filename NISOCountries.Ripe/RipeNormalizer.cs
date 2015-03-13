using NISOCountries.Core.ValueNormalizers;
using System.Text;
using System.Text.RegularExpressions;

namespace NISOCountries.Ripe
{
    public class RipeNormalizer : ISOCountryNormalizer<RipeCountry>
    {
        private static Regex _fixripe = new Regex(@"[\r\n\*]", RegexOptions.Compiled);

        public RipeNormalizer()
            : base() { }

        public RipeNormalizer(NormalizeFlags normalizeFlags)
            : base(normalizeFlags) { }

        public RipeNormalizer(NormalizeFlags normalizeFlags, NormalizationForm normalizationForm)
            : base(normalizeFlags, normalizationForm) { }

        public override RipeCountry Normalize(RipeCountry value)
        {
            value.CountryName = _fixripe.Replace(value.CountryName, " ");
            return base.Normalize(value);
        }
    }
}
