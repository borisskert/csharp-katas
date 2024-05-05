using Katas.LandPerimeter;
using NUnit.Framework;

[TestFixture]
public class SampleTests
{
    [TestCase("", 0, TestName = "Sample test 1")]
    [TestCase("O", 0, TestName = "Sample test 2")]
    [TestCase("X", 4, TestName = "Sample test 3")]
    [TestCase("X X", 6, TestName = "Simple test 1")]
    [TestCase("OXOOOX OXOXOO XXOOOX OXXXOO OOXOOX OXOOOO OOXOOX OOXOOO OXOOOO OXOOXX", 60, TestName = "Sample test 4")]
    [TestCase("OXOOO OOXXX OXXOO XOOOO XOOOO XXXOO XOXOO OOOXO OXOOX XOOOO OOOXO", 52, TestName = "Sample test 5")]
    [TestCase("XXXXXOOO OOXOOOOO OOOOOOXO XXXOOOXO OXOXXOOX", 40, TestName = "Sample test 6")]
    [TestCase("XOOOXOO OXOOOOO XOXOXOO OXOXXOO OOOOOXX OOOXOXX XXXXOXO", 54, TestName = "Sample test 7")]
    [TestCase("OOOOXO XOXOOX XXOXOX XOXOOO OOOOOO OOOXOO OOXXOO", 40, TestName = "Sample test 8")]
    public void BasicTests(string testInput, int expectedArea)
    {
        string[] kataInput = testInput.Split(' ');
        string expectedAnswer = $"Total land perimeter: {expectedArea}";
        string actual = LandPerimeter.Calculate(kataInput);
        Assert.AreEqual(expectedAnswer, actual);
    }
}
