namespace Katas.StopGninnipsMySdrow
{
    using System;
    using System.Linq;

    /**
     * https://www.codewars.com/kata/5264d2b162488dc400000001/train/csharp
     */
    public class Kata
    {
        public static string SpinWords(string sentence)
        {
            var words = sentence.Split(" ")
                .Select(word => word.Length >= 5 ? Reverse(word) : word);

            return string.Join(" ", words);
        }

        private static string Reverse(string word)
        {
            var chars = word.ToCharArray();
            Array.Reverse(chars);

            return new string(chars);
        }
    }
}
