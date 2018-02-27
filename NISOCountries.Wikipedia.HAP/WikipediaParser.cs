﻿using HtmlAgilityPack;
using NISOCountries.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NISOCountries.Wikipedia.HAP
{
    public class WikipediaParser : IStreamParser<WikipediaCountry>
    {
        public IEnumerable<WikipediaCountry> Parse(StreamReader streamReader)
        {
            return ExtractFromTables(streamReader.ReadToEnd());
        }

        private IEnumerable<WikipediaCountry> ExtractFromTables(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            foreach (var t in QueryNodes(doc.DocumentNode, "//table[contains(@class, 'wikitable')]"))
            {
                //TODO: Be a bit more selective on which tables to use (not only cells.length>5 but "scan" header-row for specific text for example)

                foreach (var r in QueryNodes(t, "tr"))
                {
                    var cells = QueryNodes(r, "td").ToArray();
                    //Do we have enough data?
                    if (cells.Length >= 5 && cells[4].InnerText.StartsWith("ISO 3166-2:", StringComparison.OrdinalIgnoreCase))
                    {
                        yield return new WikipediaCountry
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
