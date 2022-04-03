namespace Katas.DirectionsReduction
{
    using System.Diagnostics.Contracts;
    using System.Collections.Generic;
    using System.Linq;
    using System;

    /// <summary>
    /// https://www.codewars.com/kata/550f22f4d758534c1100025a/train/csharp
    /// Implementing this solution by following these rules:
    ///   - No (for|foreach|while) loops
    ///   - Avoid if-else statements
    ///   - Make all classes immutable
    /// </summary>
    public class DirReduction
    {
        public static string[] dirReduc(String[] arr)
        {
            return arr
                .Select(Enum.Parse<Direction>)
                .Aggregate(Directions.Empty(), (directions, direction) => directions.Append(direction))
                .AsEnumerable()
                .Select(direction => direction.ToString())
                .ToArray();
        }
    }

    internal class Directions
    {
        private readonly IEnumerable<Direction> _directions;

        private Directions(IEnumerable<Direction> directions)
        {
            _directions = directions;
        }

        [Pure]
        public Directions Append(Direction direction)
        {
            if (!_directions.Any())
            {
                return new Directions(new List<Direction> {direction});
            }

            var last = _directions.Last();

            return last.IsOppositeOf(direction)
                ? new Directions(_directions.SkipLast(1))
                : new Directions(_directions.Append(direction));
        }

        [Pure]
        public IEnumerable<Direction> AsEnumerable()
        {
            return _directions.AsEnumerable();
        }

        public static Directions Empty()
        {
            return new Directions(new List<Direction>());
        }
    }

    internal enum Direction
    {
        NORTH,
        SOUTH,
        EAST,
        WEST
    }

    internal static class DirectionMethods
    {
        private static readonly IDictionary<Direction, Direction> Opposites = new Dictionary<Direction, Direction>
        {
            {Direction.NORTH, Direction.SOUTH},
            {Direction.SOUTH, Direction.NORTH},
            {Direction.EAST, Direction.WEST},
            {Direction.WEST, Direction.EAST}
        };

        [Pure]
        public static bool IsOppositeOf(this Direction direction, Direction other)
        {
            return Opposites[direction].Equals(other);
        }
    }
}
