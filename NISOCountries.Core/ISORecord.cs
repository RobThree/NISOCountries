using System.Diagnostics;

namespace NISOCountries.Core
{
    [DebuggerDisplay("Alpha2: {Alpha2} Alpha3: {Alpha3} Numeric: {Numeric} Country: {CountryName}")]
    public class ISORecord : IISORecord
    {
        public string Alpha2 { get; set; }
        public string Alpha3 { get; set; }
        public string Numeric { get; set; }
        public string CountryName { get; set; }
    }
}