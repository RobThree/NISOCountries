﻿using NISOCountries.Core;
using NISOCountries.Core.SourceProviders;
using NISOCountries.Core.ValueNormalizers;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace NISOCountries.Tests
{
    public static class TestUtil
    {
        public static ISourceProvider GetTestFileReader(Encoding encoding = null)
        {
            return new FileSource(encoding ?? Encoding.UTF8);
        }
    }

    public class TestCountry : ISOCountry
    {

    }

    public class TestCountryReader : ISOCountryReader<TestCountry>
    {
        public TestCountryReader()
            : base(new TestParser(), new TestCountrySource()) { }

        public TestCountryReader(IValueNormalizer<TestCountry> valueNormalizer)
            : base(new TestParser(), valueNormalizer, new TestCountrySource()) { }

        public override IEnumerable<TestCountry> GetDefault()
        {
            return Parse(null);
        }
    }

    public class TestCountrySource : ISourceProvider
    {
        private static TestCountry[] _countries =         {
            new TestCountry { Alpha2 = "TC", Alpha3 = "TST", Numeric = "001", CountryName = "TestCountry" },
            new TestCountry { Alpha2 = "QQ", Alpha3 = "QQQ", Numeric = "101", CountryName = "TestCountry QQ" }
        };

        public StreamReader GetStreamReader(string source)
        {
            var ser = new XmlSerializer(typeof(TestCountry[]));
            var mem = new MemoryStream();

            ser.Serialize(mem, _countries);
            mem.Position = 0;
            return new StreamReader(mem, Encoding.UTF8, false, 1024, true);
        }
    }

    public class TestParser : IStreamParser<TestCountry>
    {

        public IEnumerable<TestCountry> Parse(StreamReader streamReader)
        {
            var ser = new XmlSerializer(typeof(TestCountry[]));
            foreach (var c in (TestCountry[])ser.Deserialize((TextReader)streamReader))
                yield return c;
            streamReader.Close();
        }
    }

    public class TestLowercasingValueConverter : ISOCountryNormalizer<TestCountry>
    {
        public override TestCountry Normalize(TestCountry value)
        {
            return new TestCountry
            {
                Alpha2 = value.Alpha2.ToLowerInvariant(),
                Alpha3 = value.Alpha3.ToLowerInvariant(),
                CountryName = value.CountryName.ToLowerInvariant(),
                Numeric = value.Numeric
            };
        }

    }
}
