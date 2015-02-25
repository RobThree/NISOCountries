using NISOCountries.Core.ValueNormalizers;

namespace NISOCountries.GeoNames
{
    public class GeonamesNormalizer : BaseNormalizer<GeonamesRecord>
    {
        public override GeonamesRecord Normalize(GeonamesRecord value)
        {
            value = base.Normalize(value);
            value.Fips = NormalizeString(value.Fips, NormalizeFlags.Default | NormalizeFlags.ToUpper);
            return value;
        }
    }
}
