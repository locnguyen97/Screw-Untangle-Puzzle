using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringHelper
{

    static Dictionary<int, string> s_MapNumber = new Dictionary<int, string>();

    public static string GetNumber(int number)
    {
        if (!s_MapNumber.ContainsKey(number))
        {
            s_MapNumber[number] = number.ToString();

        }
        return s_MapNumber[number];
    }


    public static string RemoveBetweenParenthesis(this string original)
    {
        return RemoveBetweenString(original, "(", ")");
    }
    public static string RemoveBetweenString(this string original, string start, string end)
    {
        var pFrom = original.IndexOf(start, StringComparison.Ordinal) + start.Length;
        var pTo = original.IndexOf(end, pFrom, StringComparison.Ordinal);

        if (pFrom == -1 || pTo == -1 || pFrom >= pTo) return original;
        return original.Remove(pFrom - start.Length, pTo - pFrom + start.Length + end.Length);
    }

    public static string GetBetweenString(this string original, string start, string end,
        bool includeStartEndString = true)
    {
        var pFrom = original.IndexOf(start, StringComparison.Ordinal);
        var pTo = original.IndexOf(end, pFrom + start.Length, StringComparison.Ordinal);
        if (end == string.Empty || end == "") pTo = original.Length;

        if (pFrom == -1 || pTo == -1) return "";
        return includeStartEndString
            ? original.Substring(pFrom, pTo - pFrom + end.Length)
            : original.Substring(pFrom + start.Length, pTo - pFrom - start.Length);
    }
}