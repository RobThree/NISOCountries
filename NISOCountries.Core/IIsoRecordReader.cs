using NISOCountries.Core.ValueNormalizers;
using System.Collections.Generic;

namespace NISOCountries.Core
{
    public interface IISOCountryReader<T>
        where T : IISOCountry
    {
        IValueNormalizer<T> ValueNormalizer { get; }
        IEnumerable<T> Parse(string filename);
        IStreamParser<T> StreamParser { get; }
    }
}
