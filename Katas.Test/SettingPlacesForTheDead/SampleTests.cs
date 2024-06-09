using System;
using Katas.SettingPlacesForTheDead;
using NUnit.Framework;

namespace Katas.Test.SettingPlacesForTheDead;

[TestFixture]
public class SampleTests
{
    [Test(Description = "Test 1 ~ Artlu only")]
    public void Test1()
    {
        String[] theDead = { "Artlu" };
        String[] expected =
        {
            "_____", "_____", "_____", "_____", "_____", "_____", "Artlu", "_____", "_____", "_____", "_____", "_____"
        };
        String[] submitted = GreatHall.SetTable(theDead);
        Assert.AreEqual(expected, submitted);
    }

    [Test(Description = "Test 2 ~ Artlu, Breca, Cityl, and Dedaf")]
    public void Test2()
    {
        String[] theDead = { "Artlu", "Breca", "Cityl", "Dedaf" };
        String[] expected =
        {
            "Cityl", "_____", "_____", "_____", "_____", "Breca", "Artlu", "_____", "_____", "_____", "_____", "Dedaf"
        };
        String[] submitted = GreatHall.SetTable(theDead);
        Assert.AreEqual(expected, submitted);
    }

    [Test(Description = "Test 3 ~ All Favor the Same Feature")]
    public void Test3()
    {
        String[] theDead =
        {
            "Sevap", "Syolc", "Sgulg", "Stolb", "Sknoh", "Spord", "Sgnaf", "Shcat", "Sknit", "Snirg", "Senin", "Sliob"
        };
        String[] expected =
        {
            "Sgnaf", "Sknit", "Senin", "Sliob", "Snirg", "Shcat", "Spord", "Stolb", "Syolc", "Sevap", "Sgulg", "Sknoh"
        };
        String[] submitted = GreatHall.SetTable(theDead);
        Assert.AreEqual(expected, submitted);
    }

    [Test(Description = "Test 4 ~ Example From the Description")]
    public void Test4()
    {
        String[] theDead =
        {
            "Yojne", "Xenna", "Verap", "Ebyam", "Teseb", "Ycuag", "Onets", "Skcaw", "Yrovi", "Tpets", "Lizuf", "Girnu"
        };
        String[] expected =
        {
            "Teseb", "Onets", "Verap", "Xenna", "Ebyam", "Ycuag", "Yojne", "Yrovi", "Lizuf", "Skcaw", "Girnu", "Tpets"
        };
        String[] submitted = GreatHall.SetTable(theDead);
        Assert.AreEqual(expected, submitted);
    }

    [Test(Description = "Test 5 ~ Too Many Ghosts to Seat")]
    public void Test5()
    {
        String[] theDead =
        {
            "Egdob", "Liame", "Skceg", "Yesba", "Cinid", "Sallo", "Sumac", "Triks", "Sipat", "Elona", "Sreod", "Deyab",
            "Dlaps", "Nevey", "Htron"
        };
        String[] expected =
        {
            "Cinid", "Sreod", "Elona", "Egdob", "Deyab", "Yesba", "Liame", "Sipat", "Sallo", "Skceg", "Sumac", "Triks"
        };
        String[] submitted = GreatHall.SetTable(theDead);
        Assert.AreEqual(expected, submitted);
    }
}
