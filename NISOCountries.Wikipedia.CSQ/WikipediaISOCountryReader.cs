using NISOCountries.Core;
using NISOCountries.Core.SourceProviders;
using System.Collections.Generic;

namespace NISOCountries.Wikipedia.CSQ
{
    public class WikipediaISOCountryReader : ISOCountryReader<WikipediaCountry>
    {
        public WikipediaISOCountryReader()
            : base(new WikipediaParser(), new WikipediaNormalizer()) { }

        public WikipediaISOCountryReader(ISourceProvider sourceProvider)
            : base(new WikipediaParser(), new WikipediaNormalizer(), sourceProvider) { }

        public override IEnumerable<WikipediaCountry> GetDefault()
        {
            return this.Parse(@"https://en.wikipedia.org/wiki/ISO_3166-1");
        }
    }
}
