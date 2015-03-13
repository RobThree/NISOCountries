using NISOCountries.Core;

namespace NISOCountries.Wikipedia
{
    public class WikipediaComparer : ISOCountryComparer<WikipediaCountry>
    {
        public WikipediaComparer()
            : base() { }

        public WikipediaComparer(bool ignoreCase)
            : base(ignoreCase) { }

        public override bool Equals(WikipediaCountry x, WikipediaCountry y)
        {
            // Let base handle reference equality etc.
            return base.Equals(x, y)
                // Compare properties
                //&& string.Equals(x.Foo, y.Foo);
                //&& string.Equals(x.Bar, y.Bar);
                ;
        }

        public override int GetHashCode(WikipediaCountry obj)
        {
            if (obj == null)
                return 0;

            unchecked // Overflow is fine, just wrap
            {
                int hash = (int)2166136261;
                string n = string.Empty;
                hash = hash * 16777619 ^ base.GetHashCode(obj);
                //hash = hash * 16777619 ^ (obj.Foo ?? n).GetHashCode();
                //hash = hash * 16777619 ^ (obj.Bar ?? n).GetHashCode();
                return hash;
            }
        }
    }
}
