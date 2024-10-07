using System.Linq;

namespace ALC.Core.Utils;

public static class StringUtils
{
    public static string OnlyNumbers(this string str)
    {
        return string.IsNullOrEmpty(str)
            ? string.Empty
            : new string(str.Where(char.IsDigit).ToArray());
    }
}
