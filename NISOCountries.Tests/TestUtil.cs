using NISOCountries.Core.SourceProviders;
using System.Text;

namespace NISOCountries.Tests
{
    public static class TestUtil
    {
        public static ISourceProvider GetTestFileReader(Encoding encoding = null)
        {
            return new FileSource(encoding ?? Encoding.UTF8);
        }
    }
}
