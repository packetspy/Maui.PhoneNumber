using System.Text;

namespace Maui.PhoneNumber;

public static class PhoneWordTranslator
{
    static readonly string[] digits = { "ABC", "DEF", "GHI", "JKL", "MNO", "PQRS", "TUV", "WXYZ" };

    public static string ToNumber(string raw)
    {
        if (string.IsNullOrWhiteSpace(raw))
            return null;

        raw = raw.ToUpperInvariant();

        var newNumber = new StringBuilder();
        foreach (var c in raw)
        {
            if (" -123456789".ContainsChar(c))
                newNumber.Append(c);
            else
            {
                var result = TranslateToNumber(c);
                if (result != null)
                    newNumber.Append(result);
                else
                    return null;
            }
        }
        return newNumber.ToString();
    }

    static bool ContainsChar(this string keyString, char c) => keyString.IndexOf(c) >= 0;

    static int? TranslateToNumber(char c)
    {
        for (var i = 0; i < digits.Length; i++)
            if (digits[i].Contains(c))
                return 2 + i;

        return null;
    }
}
