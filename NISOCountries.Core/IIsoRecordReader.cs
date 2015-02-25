using NISOCountries.Core.ValueNormalizers;
using System.Collections.Generic;

namespace NISOCountries.Core
{
    public interface IISORecordReader<T>
        where T : IISORecord
    {
        IValueNormalizer<T> ValueNormalizer { get; }
        IEnumerable<T> Parse(string filename);
    }
}
