using NUnit.Framework;

namespace Katas.Test.Scramblies
{
    [TestFixture]
    public static class ScrambliesTests 
    {

        private static void testing(bool actual, bool expected) 
        {
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public static void test1() 
        {
            testing(Katas.Scramblies.Scramblies.Scramble("rkqodlw","world"), true);
            testing(Katas.Scramblies.Scramblies.Scramble("cedewaraaossoqqyt","codewars"),true);
            testing(Katas.Scramblies.Scramblies.Scramble("katas","steak"),false);
            testing(Katas.Scramblies.Scramblies.Scramble("scriptjavx","javascript"),false);
            testing(Katas.Scramblies.Scramblies.Scramble("scriptingjava","javascript"),true);
            testing(Katas.Scramblies.Scramblies.Scramble("scriptsjava","javascripts"),true);
            testing(Katas.Scramblies.Scramblies.Scramble("javscripts","javascript"),false);
            testing(Katas.Scramblies.Scramblies.Scramble("aabbcamaomsccdd","commas"),true);
            testing(Katas.Scramblies.Scramblies.Scramble("commas","commas"),true);
            testing(Katas.Scramblies.Scramblies.Scramble("sammoc","commas"),true);
        }
    }

}
