using HtmlAgilityPack;
using NISOCountries.Core;
using NISOCountries.Core.SourceProviders;
using NISOCountries.Core.ValueNormalizers;
using System.Collections.Generic;
using System.Linq;

namespace NISOCountries.Wikipedia.HAP
{
    public class WikipediaReader : BaseReader<WikipediaRecord>
    {
        public const string DEFAULTURL = @"https://en.wikipedia.org/wiki/ISO_3166-1";

        public WikipediaReader()
            : this(new WikipediaNormalizer()) { }

        public WikipediaReader(IValueNormalizer<WikipediaRecord> valueNormalizer)
            : this(valueNormalizer, new CachingWebSource())
        { }

        public WikipediaReader(ISourceProvider sourceProvider)
            : this(new WikipediaNormalizer(), sourceProvider)
        { }

        public WikipediaReader(IValueNormalizer<WikipediaRecord> valueNormalizer, ISourceProvider sourceProvider)
            : base(valueNormalizer, sourceProvider)
        { }

        public override IEnumerable<WikipediaRecord> Parse(string source)
        {
            using (var s = this.SourceProvider.GetStreamReader(source))
            {
                return ExtractFromTables(s.ReadToEnd())
                    .Select(v => this.ValueNormalizer.Normalize(v));
            }
        }

        private IEnumerable<WikipediaRecord> ExtractFromTables(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            foreach (var t in doc.DocumentNode.SelectNodes("//table"))
            {
                //TODO: Be a bit more selective on which tables to use (not only cells.length>4 but "scan" header-row for specific text for example)

                foreach (var r in t.SelectNodes("tr"))
                {
                    var tds = r.SelectNodes("td");
                    if (tds != null)
                    {
                        var cells = tds.ToArray();
                        //Do we have enough data?
                        if (cells.Length >= 4)
                        {
                            yield return new WikipediaRecord
                            {
                                CountryName = cells[0].LastChild.InnerText,
                                Alpha2 = cells[1].InnerText,
                                Alpha3 = cells[2].InnerText,
                                Numeric = cells[3].InnerText,
                            };
                        }
                    }
                }
            }
        }
    }
}
