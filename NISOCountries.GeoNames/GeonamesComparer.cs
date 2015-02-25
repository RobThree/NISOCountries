using NISOCountries.Core;
using System;
using System.Collections.Generic;

namespace NISOCountries.GeoNames
{
    public class GeonamesComparer : ISORecordComparer<GeonamesRecord>
    {
        public GeonamesComparer()
            : base() { }

        public GeonamesComparer(bool ignoreCase)
            : base(ignoreCase) { }

        public override bool Equals(GeonamesRecord x, GeonamesRecord y)
        {
            // Let base handle reference equality etc.
            return base.Equals(x, y)
                // Compare properties
                && string.Equals(x.Fips, y.Fips, this.StringComparison);
        }

        public override int GetHashCode(GeonamesRecord obj)
        {
            if (obj == null)
                return 0;

            unchecked // Overflow is fine, just wrap
            {
                int hash = (int)2166136261;
                string n = string.Empty;
                hash = hash * 16777619 ^ base.GetHashCode(obj);
                hash = hash * 16777619 ^ GetStringHash(obj.Fips);
                return hash;
            }
        }
    }
}
