using NISOCountries.Core.ValueNormalizers;
using System.Text;

namespace NISOCountries.Wikipedia
{
    public class WikipediaNormalizer : ISOCountryNormalizer<WikipediaCountry>
    {
        public WikipediaNormalizer()
            : base() { }

        public WikipediaNormalizer(NormalizeFlags normalizeFlags)
            : base(normalizeFlags) { }

        public WikipediaNormalizer(NormalizeFlags normalizeFlags, NormalizationForm normalizationForm)
            : base(normalizeFlags, normalizationForm) { }
    }
}
