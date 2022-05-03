namespace MazeRunner
{
    using NUnit.Framework;
    using System;

    namespace CodeWars
    {
        [TestFixture]
        class KataTestClass
        {
            private int[,] maze = new int[,]
            {
                {1, 1, 1, 1, 1, 1, 1},
                {1, 0, 0, 0, 0, 0, 3},
                {1, 0, 1, 0, 1, 0, 1},
                {0, 0, 1, 0, 0, 0, 1},
                {1, 0, 1, 0, 1, 0, 1},
                {1, 0, 0, 0, 0, 0, 1},
                {1, 2, 1, 0, 1, 0, 1}
            };

            [TestCase]
            public void FinishTest1()
            {
                string[] directions = new string[] {"N", "N", "N", "N", "N", "E", "E", "E", "E", "E"};
                Kata test = new Kata();
                string result = test.mazeRunner(maze, directions);
                Assert.AreEqual("Finish", result, "Should return: 'Finish'");
            }

            [TestCase]
            public void FinishTest2()
            {
                string[] directions = new string[]
                    {"N", "N", "N", "N", "N", "E", "E", "S", "S", "E", "E", "N", "N", "E"};
                Kata test = new Kata();
                string result = test.mazeRunner(maze, directions);
                Assert.AreEqual("Finish", result, "Should return: 'Finish'");
            }

            [TestCase]
            public void FinishTest3()
            {
                string[] directions = new string[] {"N", "N", "N", "N", "N", "E", "E", "E", "E", "E", "W", "W"};
                Kata test = new Kata();
                string result = test.mazeRunner(maze, directions);
                Assert.AreEqual("Finish", result, "Should return: 'Finish'");
            }

            [TestCase]
            public void DeadTest1()
            {
                string[] directions = new string[] {"N", "N", "N", "W", "W"};
                Kata test = new Kata();
                string result = test.mazeRunner(maze, directions);
                Assert.AreEqual("Dead", result, "Should return: 'Dead'");
            }

            [TestCase]
            public void DeadTest2()
            {
                string[] directions = new string[] {"N", "N", "N", "N", "N", "E", "E", "S", "S", "S", "S", "S", "S"};
                Kata test = new Kata();
                string result = test.mazeRunner(maze, directions);
                Assert.AreEqual("Dead", result, "Should return: 'Dead'");
            }

            [TestCase]
            public void LostTest1()
            {
                string[] directions = new string[] {"N", "E", "E", "E", "E"};
                Kata test = new Kata();
                string result = test.mazeRunner(maze, directions);
                Assert.AreEqual("Lost", result, "Should return: 'Lost'");
            }
        }
    }
}
