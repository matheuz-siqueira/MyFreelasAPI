using System.Globalization;

namespace myfreelas.Extension;

public static class StringExtension
{
    public static bool CompareWithIgnoreCase(this string source, string search)
    {
        var index = CultureInfo.CurrentCulture.CompareInfo
            .IndexOf(source, search, CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace);
        return index >= 0 ; 
    } 
}
