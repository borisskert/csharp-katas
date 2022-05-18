using System.Collections.Generic;
using Katas.SecondVariationOnCaesarCipher;
using NUnit.Framework;

[TestFixture]
public class CaesarCipherTests
{
    [Test]
    public void Test1()
    {
        string u = "I should have known that you would have a perfect answer for me!!!";
        var encoded = CaesarTwo.encodeStr(u, 1);
        var decoded = CaesarTwo.decode(encoded);
        Assert.AreEqual(u, decoded);
    }

    [Test]
    public void Test2()
    {
        string u = "O CAPTAIN! my Captain! our fearful trip is done;";
        List<string> v = new List<string> {"opP DBQUBJ", "O! nz Dbqu", "bjo! pvs g", "fbsgvm usj", "q jt epof;"};
        Assert.AreEqual(u, CaesarTwo.decode(v));
    }

    [Test]
    public void Test3()
    {
        string u =
            "I have spread my dreams under your feet; Tread softly because you tread on my dreams. William B Yeats (1865-1939)";
        var encoded = CaesarTwo.encodeStr(u, 25);
        var decoded = CaesarTwo.decode(encoded);
        Assert.AreEqual(u, decoded);
    }
}
