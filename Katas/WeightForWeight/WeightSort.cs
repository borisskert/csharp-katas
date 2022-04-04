namespace Katas.WeightForWeight
{
    using System;
    using System.Linq;

    /// <summary>
    /// https://www.codewars.com/kata/55c6126177c9441a570000cc/train/csharp
    /// Implementing the solution by following these rules:
    ///   - No (for|foreach|while) loops
    ///   - No if-else statements
    ///   - Make all classes immutable
    /// </summary>
    public class WeightSort
    {
        public static string orderWeight(string strng)
        {
            var sortedWeights = strng.Split(" ")
                .Select(Weight.From)
                .OrderBy(s => s)
                .Select(weight => weight.RawValue)
                .ToArray();

            return string.Join(" ", sortedWeights);
        }
    }

    internal class Weight : IComparable<Weight>
    {
        private readonly int _crossSum;

        private Weight(string raw, int crossSum)
        {
            RawValue = raw;
            _crossSum = crossSum;
        }

        public string RawValue { get; }

        public int CompareTo(Weight other)
        {
            return _crossSum == other._crossSum
                ? string.Compare(RawValue, other.RawValue, StringComparison.Ordinal)
                : _crossSum.CompareTo(other._crossSum);
        }

        public static Weight From(string raw)
        {
            var crossSum = raw.ToCharArray()
                .Select(c => (int) char.GetNumericValue(c))
                .Sum();

            return new Weight(raw, crossSum);
        }
    }
}
