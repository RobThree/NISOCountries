using System;

namespace NISOCountries.Core.ValueNormalizers
{
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
        RemoveDiacritics = 0x0040,
        ToTitleCase = 0x0080,

        Default = Trim | NormalizeUnicode | StripMultiWhitespace | ToTitleCase,
        All = 0xFFFF
    }
}
