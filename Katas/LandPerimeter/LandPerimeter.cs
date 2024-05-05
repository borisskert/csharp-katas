namespace Katas.LandPerimeter;

using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// https://www.codewars.com/kata/5839c48f0cf94640a20001d3/train/csharp
/// </summary>
public static class LandPerimeter
{
    public static string Calculate(IEnumerable<string> lines)
    {
        var landPoints = LandPoints.Parse(lines);
        return $"Total land perimeter: {landPoints.Perimeter()}";
    }
}

internal class LandPoints
{
    private readonly HashSet<Point> _points;

    private LandPoints(HashSet<Point> points)
    {
        _points = points;
    }

    public int Perimeter()
    {
        return _points
            .Select(point => point.Neighbors())
            .Flatten()
            .Count(neighbor => !_points.Contains(neighbor));
    }

    public static LandPoints Parse(IEnumerable<string> lines)
    {
        var points = lines.Select(
                row => row.ToCharArray()
            )
            .Select((row, y) =>
            {
                return row.Select(
                        (c, x) => c == 'X'
                            ? Optional<Point>.Of(Point.Of(x, y))
                            : Optional<Point>.Empty()
                    )
                    .Flatten();
            }).Flatten()
            .ToHashSet();

        return new LandPoints(points);
    }
}

internal class Point
{
    private long X { get; }
    private long Y { get; }

    private Point(long x, long y)
    {
        X = x;
        Y = y;
    }

    public IEnumerable<Point> Neighbors()
    {
        return new[]
        {
            Of(X - 1, Y),
            Of(X + 1, Y),
            Of(X, Y - 1),
            Of(X, Y + 1)
        };
    }

    public override bool Equals(object obj)
    {
        return obj is Point point &&
               X == point.X &&
               Y == point.Y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public static Point Of(long x, long y)
    {
        return new Point(x, y);
    }
}

internal static class EnumerableExtensions
{
    public static IEnumerable<T> Flatten<T>(this IEnumerable<Optional<T>> source)
    {
        return source
            .Where(option => !option.IsEmpty)
            .Select(option => option.Value);
    }

    public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> source)
    {
        return source.SelectMany(x => x);
    }
}

/// <summary>
/// See: https://stackoverflow.com/a/39690487/13213024
/// </summary>
internal class Optional<T>
{
    private readonly T[] _value;

    private Optional(T[] value)
    {
        _value = value;
    }

    public T Value => _value[0];

    public static Optional<T> Of(T value)
    {
        return new Optional<T>(new[] { value });
    }

    public static Optional<T> Empty() => new(Array.Empty<T>());

    public bool IsEmpty => _value.Length == 0;
}
