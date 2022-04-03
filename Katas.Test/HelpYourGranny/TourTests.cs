using System.Collections;
using Katas.HelpYourGranny;
using NUnit.Framework;

namespace Katas.Test.HelpYourGranny
{
    [TestFixture]
    public class TourTests
    {
        [Test]
        public void Test1()
        {
            string[] friends1 = {"A1", "A2", "A3", "A4", "A5"};
            string[][] fTowns1 =
            {
                new[] {"A1", "X1"}, new[] {"A2", "X2"}, new[] {"A3", "X3"},
                new[] {"A4", "X4"}
            };
            Hashtable distTable1 = new Hashtable {{"X1", 100.0}, {"X2", 200.0}, {"X3", 250.0}, {"X4", 300.0}};
            Assert.AreEqual(889, Tour.tour(friends1, fTowns1, distTable1));
        }
    }
}
