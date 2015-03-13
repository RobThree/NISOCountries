using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NISOCountries.Core.ValueNormalizers
{
    public class ISOCountryNormalizer<T> : IValueNormalizer<T>
            where T : IISOCountry, new()
    {
        private static Regex _stripmultiwhitespace = new Regex(@"\s{2,}", RegexOptions.Compiled | RegexOptions.CultureInvariant);
        private static Regex _stripsymbols = new Regex(@"[^\w\s]", RegexOptions.Compiled | RegexOptions.CultureInvariant);

        public NormalizationForm NormalizationForm { get; private set; }
        public NormalizeFlags NormalizeFlags { get; private set; }

        public ISOCountryNormalizer()
            : this(NormalizeFlags.All) { }

        public ISOCountryNormalizer(NormalizeFlags normalizeFlags)
            : this(normalizeFlags, NormalizationForm.FormC) { }

        public ISOCountryNormalizer(NormalizeFlags normalizeFlags, NormalizationForm normalizationForm)
        {
            //http://unicode.org/reports/tr15/#Norm_Forms
            this.NormalizationForm = normalizationForm;
            this.NormalizeFlags = normalizeFlags;
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
            return value.Normalize(this.NormalizationForm);
        }


        private static string ToUpper(string value)
        {
            return value.ToUpperInvariant();
        }

        private static string ToLower(string value)
        {
            return value.ToLowerInvariant();
        }

        private static string RemoveDiacritics(string value)
        {
            value = value.Normalize(NormalizationForm.FormD);
            var chars = value.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();
            return new string(chars).Normalize(NormalizationForm.FormC);
        }

        protected string NormalizeString(string value)
        {
            return NormalizeString(value, NormalizeFlags.Default);
        }

        protected string NormalizeString(string value, NormalizeFlags normalizeFlags)
        {
            if (value == null)
                return null;

            if (normalizeFlags.HasFlag(NormalizeFlags.RemoveDiacritics))
                value = RemoveDiacritics(value);

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
                value.CountryName = NormalizeString(value.CountryName, this.NormalizeFlags);
            }
            return value;
        }
    }
}
