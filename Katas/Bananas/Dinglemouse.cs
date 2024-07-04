namespace Katas.Bananas;

using System.Collections.Generic;
using System.Linq;

/// <summary>
/// https://www.codewars.com/kata/5917fbed9f4056205a00001e/train/csharp
/// </summary>
public class Dinglemouse
{
    public static HashSet<string> Bananas(string text)
    {
        return Search("banana", text)
            .ToHashSet();
    }

    private static ISet<string> Search(IEnumerable<char> banana, IEnumerable<char> text)
    {
        if (!banana.Any())
        {
            return new HashSet<string> { text.Masked() };
        }

        if (!text.Any())
        {
            return new HashSet<string>();
        }

        if (banana.First() != text.First())
        {
            return OmitOneLetter(banana, text)
                .ToHashSet();
        }

        var taken = TakeOneLetter(banana, text);
        var omitted = OmitOneLetter(banana, text);

        return taken.Concat(omitted).ToHashSet();
    }

    private static IEnumerable<string> TakeOneLetter(IEnumerable<char> banana, IEnumerable<char> text)
    {
        return Search(banana.Skip(1), text.Skip(1))
            .Select(text => banana.First() + text);
    }

    private static IEnumerable<string> OmitOneLetter(IEnumerable<char> banana, IEnumerable<char> text)
    {
        return Search(banana, text.Skip(1))
            .Select(text => "-" + text);
    }
}

internal static class EnumerableExtensions
{
    public static string Masked(this IEnumerable<char> text)
    {
        return text.Select(_ => "-")
            .Aggregate("", (acc, x) => acc + x);
    }
}
