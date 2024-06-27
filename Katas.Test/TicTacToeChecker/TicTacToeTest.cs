using Katas.TicTacToeChecker;
using NUnit.Framework;

namespace Katas.Test.TicTacToeChecker;

[TestFixture]
public class TicTacToeTest
{
    private readonly TicTacToe _tictactoe = new TicTacToe();

    [Test]
    public void Test1()
    {
        int[,] board = { { 1, 1, 1 }, { 0, 2, 2 }, { 0, 0, 0 } };
        Assert.AreEqual(1, _tictactoe.IsSolved(board));
    }

    [Test]
    public void FailingTest()
    {
        int[,] board = { { 0, 0, 2 }, { 0, 0, 0 }, { 1, 0, 1 } };
        Assert.AreEqual(-1, _tictactoe.IsSolved(board));
    }
}
