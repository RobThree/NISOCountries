using System.Collections.Generic;
using System.IO;

namespace NISOCountries.Core
{
    public interface IStreamParser<T>
        where T : IISOCountry

    {
        IEnumerable<T> Parse(StreamReader streamReader);
    }

}
