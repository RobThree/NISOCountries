using CsQuery;
using NISOCountries.Core;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NISOCountries.Wikipedia.CSQ
{
    public class WikipediaParser : IStreamParser<WikipediaRecord>
    {
        public const string DEFAULTURL = @"https://en.wikipedia.org/wiki/ISO_3166-1";

        public IEnumerable<WikipediaRecord> Parse(StreamReader streamReader)
        {
            return ExtractFromTables(streamReader.ReadToEnd());
        }

        private IEnumerable<WikipediaRecord> ExtractFromTables(string html)
        {
            var doc = CQ.CreateDocument(html);
            foreach (var t in doc.Select("#bodyContent table.wikitable tbody"))
            {
                //TODO: Be a bit more selective on which tables to use (not only cells.length>4 but "scan" header-row for specific text for example)

                foreach (var r in t.ChildElements)
                {
                    if (r.FirstElementChild != null && r.FirstElementChild.NodeName.Equals("TD"))
                    {
                        var cells = r.ChildElements.ToArray();
                        //Do we have enough data?
                        if (cells.Length >= 4)
                        {
                            yield return new WikipediaRecord
                            {
                                CountryName = cells[0].LastChild.Cq().Text(),
                                Alpha2 = cells[1].Cq().Text(),
                                Alpha3 = cells[2].Cq().Text(),
                                Numeric = cells[3].Cq().Text(),
                            };
                        }
                    }
                }
            };
        }
    }
}
