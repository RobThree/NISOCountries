using System;
using System.Text;
using System.Text.RegularExpressions;

namespace NISOCountries.Core.ValueNormalizers
{
    public abstract class BaseNormalizer<T> : IValueNormalizer<T>
            where T : IISORecord, new()
    {
        private static Regex _stripmultiwhitespace = new Regex(@"\s{2,}", RegexOptions.Compiled | RegexOptions.CultureInvariant);
        private static Regex _stripsymbols = new Regex(@"[^\w\s]", RegexOptions.Compiled | RegexOptions.CultureInvariant);
        private NormalizationForm normalizationForm;

        [Flags]
        public enum NormalizeFlags
        {
            None = 0x0000,

            Trim = 0x0001,
            NormalizeUnicode = 0x0002,
            StripMultiWhitespace = 0x0004,
            StripSymbols = 0x0008,
            ToUpper = 0x0010,
            ToLower = 0x0020,

            Default = Trim | NormalizeUnicode | StripMultiWhitespace,
            All = 0xFFFF
        }

        public BaseNormalizer()
            : this(NormalizeFlags.All, NormalizationForm.FormC) { }

        public BaseNormalizer(NormalizeFlags normalizeFlags, NormalizationForm normalizationForm)
        {
            //http://unicode.org/reports/tr15/#Norm_Forms
            this.normalizationForm = normalizationForm;
        }

        private static string StripMultiWhitespace(string value)
        {
            return _stripmultiwhitespace.Replace(value, " ");
        }

        private static string StripSymbols(string value)
        {
            return _stripsymbols.Replace(value, " ");
        }

        private string NormalizeUnicode(string value)
        {
            return value.Normalize(this.normalizationForm);
        }


        private static string ToUpper(string value)
        {
            return value.ToUpperInvariant();
        }

        private static string ToLower(string value)
        {
            return value.ToLowerInvariant();
        }

        protected string NormalizeString(string value)
        {
            return NormalizeString(value, NormalizeFlags.Default);
        }

        protected string NormalizeString(string value, NormalizeFlags normalizeFlags)
        {
            if (value == null)
                return null;

            if (normalizeFlags.HasFlag(NormalizeFlags.NormalizeUnicode))
                value = NormalizeUnicode(value);
            if (normalizeFlags.HasFlag(NormalizeFlags.StripSymbols))
                value = StripSymbols(value);
            if (normalizeFlags.HasFlag(NormalizeFlags.StripMultiWhitespace))
                value = StripMultiWhitespace(value);

            if (normalizeFlags.HasFlag(NormalizeFlags.ToUpper))
                value = ToUpper(value);
            else if (normalizeFlags.HasFlag(NormalizeFlags.ToLower))
                value = ToLower(value);

            if (normalizeFlags.HasFlag(NormalizeFlags.Trim))
                value = value.Trim();

            return value;
        }

        public virtual T Normalize(T value)
        {
            if (value != null)
            {
                value.Alpha2 = NormalizeString(value.Alpha2, NormalizeFlags.Default | NormalizeFlags.ToUpper);
                value.Alpha3 = NormalizeString(value.Alpha3, NormalizeFlags.Default | NormalizeFlags.ToUpper);
                value.Numeric = NormalizeString(value.Numeric);
                value.CountryName = NormalizeString(value.CountryName);
            }
            return value;
        }
    }
}
