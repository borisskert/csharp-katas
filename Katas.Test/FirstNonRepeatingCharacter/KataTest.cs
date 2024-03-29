using Katas.FirstNonRepeatingCharacter;
using NUnit.Framework;

namespace Katas.Test.FirstNonRepeatingCharacter;

[TestFixture]
public class KataTest
{
    [Test]
    public void BasicTests()
    {
        Assert.AreEqual("a", Kata.FirstNonRepeatingLetter("a"));
        Assert.AreEqual("t", Kata.FirstNonRepeatingLetter("stress"));
        Assert.AreEqual("e", Kata.FirstNonRepeatingLetter("moonmen"));
        Assert.AreEqual("T", Kata.FirstNonRepeatingLetter("sTreSS"));
        Assert.AreEqual("", Kata.FirstNonRepeatingLetter("abba"));
    }    
}
