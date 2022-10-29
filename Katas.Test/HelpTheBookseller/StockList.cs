using System;
using NUnit.Framework;

namespace Katas.Test.HelpTheBookseller;

[TestFixture]
public class StockListTests
{
    [Test]
    public void Test1()
    {
        string[] art = { "ABAR 200", "CDXE 500", "BKWR 250", "BTSQ 890", "DRTY 600" };
        String[] cd = { "A", "B" };
        Assert.AreEqual("(A : 200) - (B : 1140)", StockList.stockSummary(art, cd));
    }

    [Test]
    public void Test2()
    {
        string[] art = { "BBAR 150", "CDXE 515", "BKWR 250", "BTSQ 890", "DRTY 600" };
        String[] cd = { "A", "B", "C", "D" };
        Assert.AreEqual("(A : 0) - (B : 1290) - (C : 515) - (D : 600)", StockList.stockSummary(art, cd));
    }

    [Test]
    public void TestEmpty()
    {
        string[] art = Array.Empty<string>();
        String[] cd = { "A", "B", "C", "D" };
        Assert.AreEqual(string.Empty, StockList.stockSummary(art, cd));
    }
}
