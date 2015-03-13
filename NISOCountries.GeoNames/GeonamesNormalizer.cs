using NISOCountries.Core.ValueNormalizers;
using System.Text;

namespace NISOCountries.GeoNames
{
    public class GeonamesNormalizer : ISOCountryNormalizer<GeonamesCountry>
    {
        public GeonamesNormalizer()
            : base() { }

        public GeonamesNormalizer(NormalizeFlags normalizeFlags)
            : base(normalizeFlags) { }

        public GeonamesNormalizer(NormalizeFlags normalizeFlags, NormalizationForm normalizationForm)
            : base(normalizeFlags, normalizationForm) { }

        public override GeonamesCountry Normalize(GeonamesCountry value)
        {
            value = base.Normalize(value);
            value.Fips = NormalizeString(value.Fips, NormalizeFlags.Default | NormalizeFlags.ToUpper);
            return value;
        }
    }
}
