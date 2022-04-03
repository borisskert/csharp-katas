using Katas.MaximumSubarraySum;
using NUnit.Framework;

namespace Katas.Test.MaximumSubarraySum
{
    [TestFixture]
    public class SolutionTest
    {
        [Test]
        public void Test0()
        {
            Assert.AreEqual(0, Kata.MaxSequence(new int[0]));
        }

        [Test]
        public void Test1()
        {
            Assert.AreEqual(6, Kata.MaxSequence(new int[] {-2, 1, -3, 4, -1, 2, 1, -5, 4}));
        }
        
        [Test]
        public void Test2()
        {
            Assert.AreEqual(155, Kata.MaxSequence(new int[] {7, 4, 11, -11, 39, 36, 10, -6, 37, -10, -32, 44, -26, -34, 43, 43}));
        }     
        
        [Test]
        public void RandomTest()
        {
            Assert.AreEqual(127, Kata.MaxSequence(new int[] {7, 32, 18, 33, -7, -22, -21, -20, 25, -25, 7, -39, -36, 28, -12, 6, 5, -24, -3, 5, -23, 23, -38, -15, -32, -18, -3, -31, -24, -33, -33, -25, -30, -11, 19, 12, 29, -40, 1, 14, -13, -6, 12, -17, 28, 9, 26, 4, 28, 21}));
        }
    }
}
