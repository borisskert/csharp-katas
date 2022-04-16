using System;
using NUnit.Framework;

namespace Katas.Test.JosephusSurvivor
{
    [TestFixture]
    public static class JosephusSurvivorTests
    {
        private static void testing(int actual, int expected)
        {
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public static void test1()
        {
            Console.WriteLine("Basic Tests JosSurvivor");
            testing(Katas.JosephusSurvivor.JosephusSurvivor.JosSurvivor(7, 3), 4);
            testing(Katas.JosephusSurvivor.JosephusSurvivor.JosSurvivor(11, 19), 10);
            testing(Katas.JosephusSurvivor.JosephusSurvivor.JosSurvivor(40, 3), 28);
            testing(Katas.JosephusSurvivor.JosephusSurvivor.JosSurvivor(14, 2), 13);
            testing(Katas.JosephusSurvivor.JosephusSurvivor.JosSurvivor(100, 1), 100);
            testing(Katas.JosephusSurvivor.JosephusSurvivor.JosSurvivor(1, 300), 1);
            testing(Katas.JosephusSurvivor.JosephusSurvivor.JosSurvivor(2, 300), 1);
            testing(Katas.JosephusSurvivor.JosephusSurvivor.JosSurvivor(5, 300), 1);
            testing(Katas.JosephusSurvivor.JosephusSurvivor.JosSurvivor(7, 300), 7);
            testing(Katas.JosephusSurvivor.JosephusSurvivor.JosSurvivor(300, 300), 265);
        }
    }
}
