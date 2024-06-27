namespace Katas.TicTacToeChecker;

using System.Collections.Generic;
using System.Linq;

/// <summary>
/// https://www.codewars.com/kata/525caa5c1bf619d28c000335/train/csharp
/// </summary>
public class TicTacToe
{
    public int IsSolved(int[,] board)
    {
        var myBoard = Board.Read(board);

        if (myBoard.Winner() != State.Draw)
        {
            return (int)myBoard.Winner();
        }

        if (myBoard.IsFinished())
        {
            return (int)State.Draw;
        }

        return (int)State.NotFinished;
    }
}

internal class Board
{
    private readonly IEnumerable<Line> _lines;

    private Board(IEnumerable<Line> lines)
    {
        _lines = lines;
    }

    public State Winner()
    {
        return _lines.Select(l => l.Winner())
            .FirstOrDefault(w => w != 0);
    }

    public bool IsFinished()
    {
        return _lines.All(l => l.IsFinished());
    }

    public static Board Read(int[,] board)
    {
        var lines = new List<Line>();

        lines.AddRange(Line.CreateRows(board));
        lines.AddRange(Line.CreateColumns(board));
        lines.AddRange(Line.CreateDiagonals(board));

        return new Board(lines);
    }
}

internal class Line
{
    private readonly State[] _fields;

    private Line(int[] fields)
    {
        _fields = fields.Select(i => (State)i)
            .ToArray();
    }

    public State Winner()
    {
        return _fields.Distinct().Count() == 1 ? _fields.First() : State.Draw;
    }

    public bool IsFinished()
    {
        return _fields.All(f => f != (int)State.Draw);
    }

    public static IEnumerable<Line> CreateRows(int[,] board)
    {
        for (var i = 0; i < board.GetLength(0); i++)
        {
            yield return new Line(new[] { board[i, 0], board[i, 1], board[i, 2] });
        }
    }

    public static IEnumerable<Line> CreateColumns(int[,] board)
    {
        for (var i = 0; i < board.GetLength(0); i++)
        {
            yield return new Line(new[] { board[0, i], board[1, i], board[2, i] });
        }
    }

    public static IEnumerable<Line> CreateDiagonals(int[,] board)
    {
        yield return new Line(new[] { board[0, 0], board[1, 1], board[2, 2] });
        yield return new Line(new[] { board[0, 2], board[1, 1], board[2, 0] });
    }
}

internal enum State
{
    NotFinished = -1,
    Draw = 0
}
