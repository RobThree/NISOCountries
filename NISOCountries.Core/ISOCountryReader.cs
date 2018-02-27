﻿using NISOCountries.Core.SourceProviders;
using NISOCountries.Core.ValueNormalizers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NISOCountries.Core
{
    public abstract class ISOCountryReader<T> : IISOCountryReader<T>
        where T : IISOCountry
    {
        public IValueNormalizer<T> ValueNormalizer { get; private set; }
        public ISourceProvider SourceProvider { get; private set; }
        public IStreamParser<T> StreamParser { get; private set; }

        public ISOCountryReader(IStreamParser<T> streamParser)
            : this(streamParser, new DummyNormalizer()) { }

        public ISOCountryReader(IStreamParser<T> streamParser, IValueNormalizer<T> valueNormalizer)
            : this(streamParser, valueNormalizer, new CachingWebSource()) { }

        public ISOCountryReader(IStreamParser<T> streamParser, ISourceProvider sourceProvider)
            : this(streamParser, new DummyNormalizer(), sourceProvider) { }

        public ISOCountryReader(IStreamParser<T> streamParser, IValueNormalizer<T> valueNormalizer, ISourceProvider sourceProvider)
        {
            ValueNormalizer = valueNormalizer ?? throw new ArgumentNullException("valueNormalizer");
            SourceProvider = sourceProvider ?? throw new ArgumentNullException("sourceProvider");
            StreamParser = streamParser ?? throw new ArgumentNullException("streamParser");
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

        public IEnumerable<T> Parse(string source)
        {
            using (var s = SourceProvider.GetStreamReader(source))
            {
                return StreamParser.Parse(s)
                    .Select(v => ValueNormalizer.Normalize(v));
            }
        }

        public abstract IEnumerable<T> GetDefault();
    }
}