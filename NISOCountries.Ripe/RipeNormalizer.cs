using NISOCountries.Core.ValueNormalizers;
using System.Text;

namespace NISOCountries.Ripe
{
    public class RipeNormalizer : ISOCountryNormalizer<RipeCountry>
    {
        public RipeNormalizer()
            : base() { }

        public RipeNormalizer(NormalizeFlags normalizeFlags)
            : base(normalizeFlags) { }

        public RipeNormalizer(NormalizeFlags normalizeFlags, NormalizationForm normalizationForm)
            : base(normalizeFlags, normalizationForm) { }
    }
}
