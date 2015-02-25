using System;
using System.Collections.Generic;

namespace NISOCountries.Core
{
    public class ISORecordComparer<T> : IEqualityComparer<T>
        where T : IISORecord
    {
        private static string n = string.Empty;

        public bool IsCaseSensitive { get; private set; }
        protected StringComparison StringComparison { get { return this.IsCaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase; } }

        public ISORecordComparer()
            : this(true) { }

        public ISORecordComparer(bool ignoreCase)
        {
            this.IsCaseSensitive = !ignoreCase;
        }

        public virtual bool Equals(T x, T y)
        {
            // If reference same object including null then return true
            if (object.ReferenceEquals(x, y))
                return true;

            // If one object null the return false
            if (object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null))
                return false;

            var sc = this.StringComparison;

            // Compare properties
            return string.Equals(x.Alpha2, y.Alpha2, sc)
                && string.Equals(x.Alpha3, y.Alpha3, sc)
                && string.Equals(x.Numeric, y.Numeric, sc)
                && string.Equals(x.CountryName, y.CountryName, sc);
        }


        public virtual int GetHashCode(T obj)
        {
            if (obj == null)
                return 0;

            unchecked // Overflow is fine, just wrap
            {
                int hash = (int)2166136261;

                hash = hash * 16777619 ^ GetStringHash(obj.Alpha2);
                hash = hash * 16777619 ^ GetStringHash(obj.Alpha3);
                hash = hash * 16777619 ^ GetStringHash(obj.Numeric);
                hash = hash * 16777619 ^ GetStringHash(obj.CountryName);
                return hash;
            }
        }

        protected int GetStringHash(string value)
        {
            if (this.IsCaseSensitive)
                return (value ?? n).GetHashCode();
            else
                return (value ?? n).ToLowerInvariant().GetHashCode();
        }

        bool IEqualityComparer<T>.Equals(T x, T y)
        {
            return Equals(x, y);
        }

        int IEqualityComparer<T>.GetHashCode(T obj)
        {
            return GetHashCode(obj);
        }
    }
}
