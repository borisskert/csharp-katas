namespace Katas.MaximumSubarraySum
{
    using System;
    using System.Linq;

    /**
     * https://www.codewars.com/kata/54521e9ec8e60bc4de000d6c/train/csharp
     * Implementing this solution by following these rules:
     *   - no (for|foreach|while) loops
     *   - only immutable objects
     *   - as less if-else|switch statements as possible
     */
    public static class Kata
    {
        public static int MaxSequence(int[] arr)
        {
            return arr.Aggregate(
                Sum.Create(),
                (sum, value) => sum.Next(value)
            ).Maximum;
        }
    }

    internal class Sum
    {
        private readonly int _current;

        private Sum(int maximum, int current)
        {
            Maximum = maximum;
            _current = current;
        }

        public Sum Next(int value)
        {
            var current = Math.Max(0, _current + value);
            var maximum = Math.Max(current, Maximum);

            return new Sum(maximum, current);
        }

        public int Maximum { get; }

        public static Sum Create()
        {
            return new Sum(0, 0);
        }
    }
}
