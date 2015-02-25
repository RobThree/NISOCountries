using System;
using System.Collections.Generic;

namespace NISOCountries.Core
{
    public class ISORecordComparer<T> : IEqualityComparer<T>
        where T : IISORecord
    {
        public StringComparison StringComparison { get; private set; }

        public ISORecordComparer()
            : this(StringComparison.Ordinal) { }

        public ISORecordComparer(StringComparison stringComparison)
        {
            this.StringComparison = stringComparison;
        }

        public bool Equals(T x, T y)
        {
            // If reference same object including null then return true
            if (object.ReferenceEquals(x, y))
                return true;

            // If one object null the return false
            if (object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null))
                return false;

            // Compare properties
            return string.Equals(x.Alpha2, y.Alpha2)
                && string.Equals(x.Alpha3, y.Alpha3)
                && string.Equals(x.Numeric, y.Numeric)
                && string.Equals(x.CountryName, y.CountryName);
        }

        public int GetHashCode(T obj)
        {
            if (obj == null)
                return 0;

            unchecked // Overflow is fine, just wrap
            {
                int hash = (int)2166136261;
                string n = string.Empty;

                hash = hash * 16777619 ^ (obj.Alpha2 ?? n).GetHashCode();
                hash = hash * 16777619 ^ (obj.Alpha3 ?? n).GetHashCode();
                hash = hash * 16777619 ^ (obj.Numeric ?? n).GetHashCode();
                hash = hash * 16777619 ^ (obj.CountryName ?? n).GetHashCode();
                return hash;
            }
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
