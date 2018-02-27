using NISOCountries.Core;

namespace NISOCountries.Datahub
{
    public class DatahubCountry : ISOCountry
    {
        public string CountryNameArabic { get; set; }
        public string CountryNameEnglish { get; set; }
        public string CountryNameChinese { get; set; }
        public string CountryNameSpanish { get; set; }
        public string CountryNameFrench { get; set; }
        public string CountryNameRussian { get; set; }

        public string CLDRDisplayName { get; set; }
        public string EDGAR { get; set; }
        public string FIPS { get; set; }
        public string GlobalCode { get; set; }
        public string GlobalName { get; set; }
    }
}