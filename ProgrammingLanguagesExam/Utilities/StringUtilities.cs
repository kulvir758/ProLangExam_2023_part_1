namespace Utilities;

using System;
using System.Collections.Generic;
using System.Linq;

public static class StringUtilities
{
    public static int ToWords(string input)
    {
        string[] words = input.Split(' ');
        return words.Select(word => word.ToLower()).Distinct().Count();
    }

    public static string ToSentence(string input)
    {
        string firstChar = input.Substring(0, 1).ToUpper();
        string restOfChars = input.Substring(1).ToLower();
        return firstChar + restOfChars + ".";
    }

    public static string ToPascalCase(this string input)
    {
        string[] words = input.Split(' ');
        var textInfo = System.Globalization.CultureInfo.CurrentCulture.TextInfo;
        string result = "";

        foreach (string word in words)
        {
            result += textInfo.ToTitleCase(word.ToLower());
        }

        return result;
    }

    public static string JoinWith(this IEnumerable<string> strings, string separator)
    {
        return string.Join(separator, strings);
    }
}

