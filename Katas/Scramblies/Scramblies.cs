namespace Katas.Scramblies
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;

    /// <summary>
    /// https://www.codewars.com/kata/55c04b4cc56a697bb0000048/train/csharp
    /// Implemented this kata following these rules:
    ///   - No for|foreach|while loops
    ///   - No if|if-else|switch statements
    ///   - Immutable all the things!
    /// </summary>
    public class Scramblies
    {
        public static bool Scramble(string str1, string str2)
        {
            return Counter.Empty()
                .IncrementAll(str1)
                .DecrementAll(str2)
                .Values()
                .All(i => i >= 0);
        }
    }

    internal class Counter
    {
        private readonly ImmutableDictionary<char, int> _counts;

        private Counter(ImmutableDictionary<char, int> counts)
        {
            _counts = counts;
        }

        public Counter IncrementAll(IEnumerable<char> chars)
        {
            return chars.Aggregate(this, ((counter1, c) => counter1.Increment(c)));
        }

        private Counter Increment(char c)
        {
            var count = _counts.GetValueOrDefault(c, 0);
            return new Counter(_counts.SetItem(c, count + 1));
        }

        public Counter DecrementAll(IEnumerable<char> chars)
        {
            return chars.Aggregate(this, ((counter1, c) => counter1.Decrement(c)));
        }

        private Counter Decrement(char c)
        {
            var count = _counts.GetValueOrDefault(c, 0);
            return new Counter(_counts.SetItem(c, count - 1));
        }

        public IEnumerable<int> Values()
        {
            return _counts.Values;
        }

        public static Counter Empty()
        {
            return new Counter(ImmutableDictionary<char, int>.Empty);
        }
    }
}
