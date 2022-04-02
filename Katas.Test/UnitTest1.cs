using NUnit.Framework;

namespace Katas.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.AreEqual(Class1.Greeting(), "Hello World!");
        }
    }
}
