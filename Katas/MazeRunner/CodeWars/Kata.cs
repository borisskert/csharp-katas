namespace MazeRunner
{
    namespace CodeWars
    {
        using System;
        using System.Collections.Generic;
        using System.Collections.Immutable;
        using System.Diagnostics.Contracts;
        using System.Linq;

        /// <summary>
        /// https://www.codewars.com/kata/58663693b359c4a6560001d6/train/csharp
        /// </summary>
        public class Kata
        {
            public string mazeRunner(int[,] maze, string[] directions)
            {
                return Adventurer.Within(maze)
                    .Run(directions);
            }
        }

        internal enum Field : ushort
        {
            Wall = 1,
            Start = 2,
            Finish = 3
        }

        internal class Position
        {
            private readonly int _x;
            private readonly int _y;

            private Position(int x, int y)
            {
                _x = x;
                _y = y;
            }

            [Pure]
            public Position NeighborAt(string direction)
            {
                return direction switch
                {
                    "N" => new Position(_x, _y - 1),
                    "S" => new Position(_x, _y + 1),
                    "E" => new Position(_x + 1, _y),
                    "W" => new Position(_x - 1, _y),
                    _ => throw new ArgumentException("Unknown direction")
                };
            }

            [Pure]
            public override int GetHashCode()
            {
                return HashCode.Combine(_x, _y);
            }

            [Pure]
            public override string ToString()
            {
                return $"Position({_x}/{_y})";
            }

            [Pure]
            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((Position) obj);
            }

            private bool Equals(Position other)
            {
                return _x == other._x && _y == other._y;
            }

            public static Position From(int x, int y)
            {
                return new Position(x, y);
            }
        }

        internal class Maze
        {
            private readonly IDictionary<Position, Field> _grid;

            private Maze(IDictionary<Position, Field> grid)
            {
                _grid = grid;
            }

            [Pure]
            public Field this[Position position] => _grid.TryGetValue(position, out var value)
                ? value
                : Field.Wall;

            [Pure]
            public Position Start()
            {
                return _grid.First(pair => pair.Value == Field.Start).Key;
            }

            public static Maze FromArray(int[,] array)
            {
                return new Maze(ToDictionary(array));
            }

            private static ImmutableDictionary<Position, Field> ToDictionary(int[,] array)
            {
                IDictionary<Position, Field> dictionary = new Dictionary<Position, Field>();

                for (var y = 0; y < array.GetLength(0); y++)
                {
                    for (var x = 0; x < array.GetLength(1); x++)
                    {
                        var field = (Field) array[y, x];
                        dictionary[Position.From(x, y)] = field;
                    }
                }

                return dictionary.ToImmutableDictionary();
            }
        }

        internal class Adventurer
        {
            private readonly Maze _maze;
            private readonly Position _position;

            private Adventurer(Maze maze, Position position)
            {
                _maze = maze;
                _position = position;
            }

            [Pure]
            public string Run(string[] directions)
            {
                if (!directions.Any())
                {
                    return "Lost";
                }

                var direction = directions.First();
                var nextPosition = _position.NeighborAt(direction);
                var field = _maze[nextPosition];

                if (field == Field.Wall)
                {
                    return "Dead";
                }

                if (field == Field.Finish)
                {
                    return "Finish";
                }

                var tail = directions.AsEnumerable()
                    .Skip(1)
                    .ToArray();

                return new Adventurer(_maze, nextPosition)
                    .Run(tail);
            }

            public static Adventurer Within(int[,] array)
            {
                var maze = Maze.FromArray(array);
                var start = maze.Start();

                return new Adventurer(maze, start);
            }
        }
    }
}
