using NISOCountries.Core.ValueNormalizers;
using System.Text;

namespace NISOCountries.Wikipedia
{
    public class WikipediaNormalizer : ISORecordNormalizer<WikipediaRecord>
    {
        public WikipediaNormalizer()
            : base() { }

        public WikipediaNormalizer(NormalizeFlags normalizeFlags)
            : base(normalizeFlags) { }

        public WikipediaNormalizer(NormalizeFlags normalizeFlags, NormalizationForm normalizationForm)
            : base(normalizeFlags, normalizationForm) { }
    }
}
