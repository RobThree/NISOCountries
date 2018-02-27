using NISOCountries.Core;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NISOCountries.Datahub
{
    public class DatahubParser : IStreamParser<DatahubCountry>
    {
        public IEnumerable<DatahubCountry> Parse(StreamReader streamReader)
        {
            return streamReader.ReadAllLines()
                .Skip(1)
                .Select(l => l.SplitCSV(',').ToArray())
                .Where(v => v.Length == 56 && v[6].Length == 2 && v[7].Length == 3)
                .Select(v => new DatahubCountry
                {
                    Alpha2 = v[6],
                    Alpha3 = v[7],
                    Numeric = v[8],
                    CountryName = v[2],

                    CountryNameArabic = v[0],
                    CountryNameChinese = v[1],
                    CountryNameEnglish = v[2],
                    CountryNameSpanish = v[3],
                    CountryNameFrench = v[4],
                    CountryNameRussian = v[5],
                    CLDRDisplayName = v[27],
                    EDGAR = v[33],
                    FIPS  = v[35],
                    GlobalCode = v[38],
                    GlobalName = v[39]
                });
        }
    }

    internal static class StreamReaderExt
    {
        public static IEnumerable<string> ReadAllLines(this StreamReader sr)
        {
            var lines = new List<string>(300);  //Make an ample guess; currently there are about ~250 ISO country codes
            string line;
            while ((line = sr.ReadLine()) != null)
                lines.Add(line);
            return lines;
        }
    }

    internal static class StringExt
    {
        public static IEnumerable<string> SplitCSV(this string value, char separator)
        {
            var inquotes = false;
            var field = string.Empty;

            foreach (var c in value)
            {
                if (c == '"')
                    inquotes = !inquotes;
                if (c == separator)
                {
                    if (!inquotes)
                    {
                        yield return field;
                        field = string.Empty;
                    }
                    else
                    {
                        field += c;
                    }
                }
                else
                {
                    field += c;
                }
            }
            yield return field;
        }
    }
}