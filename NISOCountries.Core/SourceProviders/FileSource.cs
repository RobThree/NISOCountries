using System.IO;
using System.Text;

namespace NISOCountries.Core.SourceProviders
{
    public class FileSource : ISourceProvider
    {
        public Encoding Encoding { get; private set; }

        public FileSource()
            : this(Encoding.UTF8) { }

        public FileSource(Encoding encoding)
        {
            Encoding = encoding;
        }

        public StreamReader GetStreamReader(string filename)
        {
            return new StreamReader(filename, Encoding);
        }

    }
}
