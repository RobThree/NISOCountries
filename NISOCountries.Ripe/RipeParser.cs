﻿using NISOCountries.Core;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace NISOCountries.Ripe
{
    public class RipeParser : IStreamParser<RipeCountry>
    {
        private static Regex _countryregex = new Regex(@"^(?<name>(.*?\s)?.*?)\s+(?<iso2>[A-Z]{2})\s+(?<iso3>[A-Z]{3})\s+(?<numeric>[0-9]{3})\s*?", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.ExplicitCapture);
        
        public IEnumerable<RipeCountry> Parse(StreamReader streamReader)
        {
            return _countryregex.Matches(streamReader.ReadToEnd())
                .Cast<Match>()
                .Where(m => m.Success)
                .Select(m => new RipeCountry
                {
                    CountryName = m.Groups["name"].Value.Trim(),
                    Alpha2 = m.Groups["iso2"].Value,
                    Alpha3 = m.Groups["iso3"].Value,
                    Numeric = m.Groups["numeric"].Value,
                });
        }
    }
}
