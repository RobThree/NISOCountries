using NISOCountries.Core.ValueNormalizers;
using System.Text;

namespace NISOCountries.Datahub
{
    public class DatahubNormalizer : ISOCountryNormalizer<DatahubCountry>
    {
        public DatahubNormalizer()
            : base() { }

        public DatahubNormalizer(NormalizeFlags normalizeFlags)
            : base(normalizeFlags) { }

        public DatahubNormalizer(NormalizeFlags normalizeFlags, NormalizationForm normalizationForm)
            : base(normalizeFlags, normalizationForm) { }

        public override DatahubCountry Normalize(DatahubCountry value)
        {
            value = base.Normalize(value);
            value.FIPS= NormalizeString(value.FIPS, NormalizeFlags.Default | NormalizeFlags.ToUpper);
            return value;
        }
    }
}