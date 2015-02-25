using System.Collections.Generic;
using System.IO;

namespace NISOCountries.Core
{
    public interface IStreamParser<T>
        where T : IISORecord

    {
        IEnumerable<T> Parse(StreamReader streamReader);
    }

}
