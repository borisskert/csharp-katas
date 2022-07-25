namespace Katas.WhereMyAnagramsAt
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// https://www.codewars.com/kata/523a86aa4230ebb5420001e1/train/csharp
    /// </summary>
    public static class Kata
    {
        public static List<string> Anagrams(string word, List<string> words)
        {
            var sorted = word.Sorted();

            return words.FindAll(
                w => w
                    .Sorted()
                    .Equals(sorted)
            );
        }
    }

    static class StringSortExtension
    {
        public static string Sorted(this string word)
        {
            return string.Concat(
                word.AsEnumerable()
                    .OrderBy(c => c)
            );
        }
    }
}
