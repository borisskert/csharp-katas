using NUnit.Framework;

namespace Katas.GapInPrimes;

[TestFixture]
public static class GapInPrimesTests
{
    [Test]
    public static void test1()
    {
        Assert.AreEqual(new long[] { 101, 103 }, GapInPrimes.Gap(2, 100, 110));
        Assert.AreEqual(new long[] { 103, 107 }, GapInPrimes.Gap(4, 100, 110));
        Assert.AreEqual(new long[] { 101, 103 }, GapInPrimes.Gap(2, 100, 103));
        Assert.AreEqual(null, GapInPrimes.Gap(6, 100, 110));
        Assert.AreEqual(new long[] { 359, 367 }, GapInPrimes.Gap(8, 300, 400));
        Assert.AreEqual(new long[] { 337, 347 }, GapInPrimes.Gap(10, 300, 400));
    }
}
