using NISOCountries.Core.SourceProviders;
using NISOCountries.Core.ValueNormalizers;
using System;
using System.Collections.Generic;
using System.Text;

namespace NISOCountries.Core
{
    public abstract class BaseReader<T> : IISORecordReader<T>
        where T : IISORecord
    {
        public IValueNormalizer<T> ValueNormalizer { get; private set; }
        public ISourceProvider SourceProvider { get; private set; }

        public BaseReader()
            : this(new DummyNormalizer()) { }

        public BaseReader(IValueNormalizer<T> valueNormalizer)
            : this(valueNormalizer, new FileSource()) { }
        public BaseReader(IValueNormalizer<T> valueNormalizer, ISourceProvider sourceProvider)
        {
            if (valueNormalizer == null)
                throw new ArgumentNullException("valueNormalizer");
            if (sourceProvider == null)
                throw new ArgumentNullException("sourceProvider");

            this.ValueNormalizer = valueNormalizer;
            this.SourceProvider = sourceProvider;
        }

        private class DummyNormalizer : IValueNormalizer<T>
        {
            public T Normalize(T value)
            {
                return value;
            }

            public NormalizationForm NormalizationForm
            {
                get { throw new NotImplementedException(); }
            }

            public NormalizeFlags NormalizeFlags
            {
                get { throw new NotImplementedException(); }
            }
        }

        public abstract IEnumerable<T> Parse(string file);
    }
}