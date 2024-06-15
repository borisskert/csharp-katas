namespace Katas.StreetFighter2CharacterSelection;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

/// <summary>
/// https://www.codewars.com/kata/5853213063adbd1b9b0000be/train/csharp
/// </summary>
public class Kata
{
    public string[] StreetFighterSelection(string[][] fighters, int[] position, string[] moves)
    {
        var fighterSelection = FighterSelection.FromArray(fighters);
        var directions = Direction.FromArray(moves);

        var positions = directions.Scanl(
            Position.FromArray(position),
            (current, direction) => current.Move(direction)
        ).Skip(1);

        return positions
            .Select(fighterSelection.GetFighterAt)
            .Select(fighter => fighter.Name)
            .ToArray();
    }
}

internal readonly struct Position
{
    private int X { get; init; }
    private int Y { get; init; }

    internal Position Move(Direction direction)
    {
        var nextX = (X + direction.X + 6) % 6;
        var nextY = Y + direction.Y;

        if (nextY is < 0 or >= 2)
        {
            nextY = Y;
        }

        return new Position { X = nextX, Y = nextY };
    }

    public static Position FromArray(int[] array)
    {
        return new Position { X = array[0], Y = array[1] };
    }

    public static Position Of(int x, int y)
    {
        return new Position { X = x, Y = y };
    }
}

internal readonly struct Direction
{
    internal int X { get; private init; }
    internal int Y { get; private init; }

    private static Direction Up => new() { X = 0, Y = -1 };
    private static Direction Down => new() { X = 0, Y = 1 };
    private static Direction Left => new() { X = -1, Y = 0 };
    private static Direction Right => new() { X = 1, Y = 0 };

    public static IEnumerable<Direction> FromArray(string[] directions)
    {
        return directions.Select(FromString);
    }

    private static Direction FromString(string direction)
    {
        return direction switch
        {
            "up" => Up,
            "down" => Down,
            "left" => Left,
            "right" => Right,
            _ => throw new System.ArgumentException("Invalid direction", nameof(direction))
        };
    }
}

internal struct Fighter
{
    public string Name { get; init; }
}

internal class FighterSelection
{
    private readonly ImmutableDictionary<Position, Fighter> _fighters;

    private FighterSelection(ImmutableDictionary<Position, Fighter> fighters)
    {
        _fighters = fighters;
    }

    public Fighter GetFighterAt(Position position)
    {
        return _fighters[position];
    }

    public static FighterSelection FromArray(string[][] fightersArray)
    {
        var fightersDict = fightersArray
            .Select((fighters, index) => new { Fighters = fighters, Index = index })
            .SelectMany(fighter =>
                fighter.Fighters.Select((name, index) =>
                    new
                    {
                        Name = name,
                        Position = Position.Of(index, fighter.Index)
                    }
                ))
            .ToImmutableDictionary(
                fighter => fighter.Position,
                fighter => new Fighter { Name = fighter.Name }
            );

        return new FighterSelection(fightersDict);
    }
}

internal static class EnumerableExtensions
{
    public static IEnumerable<TResult> Scanl<TResult, TInput>(
        this IEnumerable<TInput> source, TResult seed, System.Func<TResult, TInput, TResult> func
    )
    {
        yield return seed;

        foreach (var item in source)
        {
            seed = func(seed, item);
            yield return seed;
        }
    }
}
