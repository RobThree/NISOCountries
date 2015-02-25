using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NISOCountries.Core
{
    public class ISOCountryLookup<T>
        where T : IISORecord
    {
        private T[] _records;
        private Dictionary<string, T> _alpha2;
        private Dictionary<string, T> _alpha3;
        private Dictionary<string, T> _numeric;
        private Dictionary<int, T> _numericasint;

        private static Regex _alpha3regex = new Regex("^[A-Z]{3}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static Regex _alpha2regex = new Regex("^[A-Z]{2}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static Regex _numericregex = new Regex("^[0-9]{1,3}$", RegexOptions.Compiled);

        public bool IsCaseSensitive { get; private set; }

        public ISOCountryLookup(IISORecordReader<T> recordreader, string source)
            : this(recordreader.Parse(source)) { }

        public ISOCountryLookup(IISORecordReader<T> recordreader, string source, bool ignoreCase)
            : this(recordreader.Parse(source), ignoreCase) { }

        public ISOCountryLookup(IEnumerable<T> countries)
            : this(countries, true) { }

        public ISOCountryLookup(IEnumerable<T> countries, bool ignoreCase)
        {
            this.IsCaseSensitive = !ignoreCase;
            var comparer = this.IsCaseSensitive ? StringComparer.Ordinal : StringComparer.OrdinalIgnoreCase;

            _records = countries.ToArray();

            _alpha2 = _records.Where(r => !string.IsNullOrEmpty(r.Alpha2)).ToDictionary(r => r.Alpha2, comparer);
            _alpha3 = _records.Where(r => !string.IsNullOrEmpty(r.Alpha3)).ToDictionary(r => r.Alpha3, comparer);
            _numeric = _records.Where(r => !string.IsNullOrEmpty(r.Numeric)).ToDictionary(r => r.Numeric, comparer);
            _numericasint = _records.Where(r => !string.IsNullOrEmpty(r.Numeric)).ToDictionary(r => int.Parse(r.Numeric));
        }

        public bool TryGetByAlpha2(string alpha2, out T result)
        {
            return _alpha2.TryGetValue(alpha2, out result);
        }

        public bool TryGetByAlpha3(string alpha3, out T result)
        {
            return _alpha3.TryGetValue(alpha3, out result);
        }

        public bool TryGetByNumeric(string numeric, out T result)
        {
            return _numeric.TryGetValue(numeric, out result);
        }

        public bool TryGetByNumeric(int numeric, out T result)
        {
            return _numericasint.TryGetValue(numeric, out result);
        }

        public T GetByAlpha2(string alpha2)
        {
            return _alpha2[alpha2];
        }

        public T GetByAlpha3(string alpha3)
        {
            return _alpha3[alpha3];
        }

        public T GetByNumeric(string numeric)
        {
            return _numeric[numeric];
        }

        public T GetByNumeric(int numeric)
        {
            return _numericasint[numeric];
        }

        public T GetByAlpha(string alpha)
        {
            if (alpha == null)
                throw new ArgumentNullException();

            if (_alpha3regex.IsMatch(alpha))
                return GetByAlpha3(alpha);
            if (_alpha2regex.IsMatch(alpha))
                return GetByAlpha2(alpha);

            throw new ArgumentException("Invalid length for alpha argument (must be either 2 or 3)", alpha);
        }

        public bool TryGetByAlpha(string alpha, out T result)
        {
            if (alpha == null)
                throw new ArgumentNullException();

            if (_alpha3regex.IsMatch(alpha))
                return TryGetByAlpha3(alpha, out result);
            if (_alpha2regex.IsMatch(alpha))
                return TryGetByAlpha2(alpha, out result);

            result = default(T);
            return false;
        }

        public T this[string code]
        {
            get { return this.Get(code); }
        }

        public T Get(string code)
        {
            if (code == null)
                throw new ArgumentNullException();

            T result;
            if (TryGet(code, out result))
                return result;

            throw new KeyNotFoundException();
        }

        public bool TryGet(string code, out T result)
        {
            if (code == null)
                throw new ArgumentNullException();

            if (_numericregex.IsMatch(code))
                return TryGetByNumeric(code, out result);

            if (TryGetByAlpha(code, out result))
                return true;

            return false;
        }

        public IEnumerable<T> Values { get { return _records; } }
    }
}
