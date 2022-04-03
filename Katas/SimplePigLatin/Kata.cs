namespace Katas.SimplePigLatin
{
    using System.Linq;

    /// <summary>
    /// https://www.codewars.com/kata/520b9d2ad5c005041100000f/train/csharp
    /// </summary>
    public class Kata
    {
        public static string PigIt(string str)
        {
            var pigWords = str.Split(" ")
                .Select(word => IsWord(word) ? ToPigWord(word) : word);

            return string.Join(" ", pigWords);
        }

        private static bool IsWord(string word)
        {
            return word.All(c => c != '!');
        }

        private static string ToPigWord(string word)
        {
            var head = word.First();
            var tail = string.Concat(word.Skip(1));

            return $"{tail}{head}ay";
        }
    }
}
