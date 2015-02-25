using System.IO;

namespace NISOCountries.Core.SourceProviders
{
    public interface ISourceProvider
    {
        StreamReader GetStreamReader(string source);
    }
}
