using Katas.DirectionsReduction;
using NUnit.Framework;

namespace Katas.Test.DirectionsReduction
{
    [TestFixture]
    public class DirReductionTests
    {
        [Test]
        public void Test1()
        {
            string[] a = {"NORTH", "SOUTH", "SOUTH", "EAST", "WEST", "NORTH", "WEST"};
            string[] b = {"WEST"};
            Assert.AreEqual(b, DirReduction.dirReduc(a));
        }

        [Test]
        public void Test2()
        {
            string[] a = {"NORTH", "WEST", "SOUTH", "EAST"};
            string[] b = {"NORTH", "WEST", "SOUTH", "EAST"};
            Assert.AreEqual(b, DirReduction.dirReduc(a));
        }
    }
}
