namespace Katas.GreedIsGood
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// https://www.codewars.com/kata/5270d0d18625160ada0000e4/train/csharp
    /// </summary>
    public static class Kata
    {
        private static readonly IDictionary<int, int> ThreeScores = new Dictionary<int, int>
        {
            {1, 1000},
            {6, 600},
            {5, 500},
            {4, 400},
            {3, 300},
            {2, 200},
        };

        private static readonly IDictionary<int, int> SingleScores = new Dictionary<int, int>
        {
            {1, 100},
            {6, 0},
            {5, 50},
            {4, 0},
            {3, 0},
            {2, 0},
        };

        public static int Score(int[] dice)
        {
            if (!dice.Any())
            {
                return 0;
            }

            var sorted = dice.OrderBy(s => s);
            var distinct = sorted.Take(3).Distinct();
            var first = distinct.First();

            int score;
            int[] remaining;

            if (sorted.Count() >= 3 && distinct.Count() == 1)
            {
                score = ThreeScores[first];
                remaining = sorted.Skip(3).ToArray();
            }
            else
            {
                score = SingleScores[first];
                remaining = sorted.Skip(1).ToArray();
            }

            return score + Score(remaining);
        }
    }
}
