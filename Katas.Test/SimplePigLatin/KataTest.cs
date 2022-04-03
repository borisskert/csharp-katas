namespace Katas.Test.SimplePigLatin
{
    using Katas.SimplePigLatin;
    using NUnit.Framework;

    [TestFixture]
    public class KataTest
    {
        [Test]
        public void KataTests()
        {
            Assert.AreEqual("igPay atinlay siay oolcay", Kata.PigIt("Pig latin is cool"));
            Assert.AreEqual("hisTay siay ymay tringsay", Kata.PigIt("This is my string"));
            Assert.AreEqual("elloHay orldWay !", Kata.PigIt("Hello World !"));
        }
    }
}
