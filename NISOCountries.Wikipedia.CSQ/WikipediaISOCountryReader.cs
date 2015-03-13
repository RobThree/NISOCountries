using NISOCountries.Core;
using NISOCountries.Core.SourceProviders;

namespace NISOCountries.Wikipedia.CSQ
{
    public class WikipediaISOCountryReader : ISOCountryReader<WikipediaCountry>
    {
        public WikipediaISOCountryReader()
            : base(new WikipediaParser(), new WikipediaNormalizer()) { }

        public WikipediaISOCountryReader(ISourceProvider sourceProvider)
            : base(new WikipediaParser(), new WikipediaNormalizer(), sourceProvider) { }

    }
}
