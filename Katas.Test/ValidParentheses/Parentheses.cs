namespace Katas.Test.ValidParentheses
{
    using Katas.ValidParentheses;
    using NUnit.Framework;

    [TestFixture]
    public class SolutionTest
    {
        [Test]
        public void SampleTest1()
        {
            Assert.AreEqual(true, Parentheses.ValidParentheses("()"));
        }

        [Test]
        public void SampleTest2()
        {
            Assert.AreEqual(false, Parentheses.ValidParentheses(")(((("));
        }
        
        [Test]
        public void RandomTests()
        {
            Assert.AreEqual(true, Parentheses.ValidParentheses("(c(b(a)))(d)"));
        }
    }
}
