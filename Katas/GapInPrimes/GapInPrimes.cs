namespace Katas.GapInPrimes;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/**
 * https://www.codewars.com/kata/561e9c843a2ef5a40c0000a4/train/csharp
 */
internal static class GapInPrimes
{
    public static long[] Gap(int g, long m, long n)
    {
        return new PrimeNumbers()
            .SkipWhile(x => x < m)
            .TakeWhile(x => x <= n)
            .Divvy(2, 1)
            .Where(pair => pair.Item2 - pair.Item1 == g)
            .Select(pair => new[] { pair.Item1, pair.Item2 })
            .FirstOrDefault();
    }
}

internal class PrimeNumbers : IEnumerable<long>
{
    private static readonly IList<long> CachedPrimes = new List<long> { 2, 3, 5, 7, 11 };

    public IEnumerator<long> GetEnumerator()
    {
        foreach (var prime in CachedPrimes)
        {
            yield return prime;
        }

        long current = CachedPrimes.Last() + 2;

        while (current < long.MaxValue)
        {
            if (IsPrime(current))
            {
                CachedPrimes.Add(current);
                yield return current;
            }

            current++;
        }
    }

    private static bool IsPrime(long number)
    {
        long sqrt = (long)Math.Sqrt(number);

        return CachedPrimes
            .TakeWhile(prime => prime <= sqrt)
            .All(prime => number % prime != 0);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

internal static class EnumerableExtensions
{
    public static IEnumerable<(T, T)> Divvy<T>(this IEnumerable<T> source, int size, int offset)
    {
        var sourceAsList = source.ToList();
        int count = sourceAsList.Count;

        for (int i = 0; i < count - size + 1; i += offset)
        {
            yield return (sourceAsList[i], sourceAsList[i + size - 1]);
        }
    }
}
