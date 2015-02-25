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
            foreach (var t in QueryNodes(doc.DocumentNode, "//table[contains(@class, 'wikitable')]"))
            {
                //TODO: Be a bit more selective on which tables to use (not only cells.length>4 but "scan" header-row for specific text for example)

                foreach (var r in QueryNodes(t, "tr"))
                {
                    var cells = QueryNodes(r, "td").ToArray();
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

        // http://htmlagilitypack.codeplex.com/workitem/29175
        // SelectNodes() returns null on no result which makes it useless for an iterator
        // This 'workaround' fixes that
        private static IEnumerable<HtmlNode> QueryNodes(HtmlNode root, string query)
        {
            var result = root.SelectNodes(query);
            if (result == null)
                return Enumerable.Empty<HtmlNode>();
            return result.AsEnumerable<HtmlNode>();
        }
    }
}
