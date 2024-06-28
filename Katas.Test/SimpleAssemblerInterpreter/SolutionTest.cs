using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Solution
{
    [TestFixture, Description("Fixed tests")]
    public class FixedTests
    {
        private void Test(Dictionary<string, int> expected, Dictionary<string, int> actual)
        {
            var expectedStr = "{ " +
                              string.Join(", ", expected.Select(kv => $"{kv.Key}: {kv.Value}"))
                              + " }";
            var actualStr = "{ " +
                            string.Join(", ", actual.Select(kv => $"{kv.Key}: {kv.Value}"))
                            + " }";

            Assert.AreEqual(expected, actual, $"Expected: {expectedStr}, Actual: {actualStr}");
        }

        [Test, Description("Should work for some example programs")]
        public void ExamplePrograms()
        {
            Test(new Dictionary<string, int> { { "a", 1 } },
                SimpleAssembler.Interpret(new[] { "mov a 5", "inc a", "dec a", "dec a", "jnz a -1", "inc a" }));
            Test(new Dictionary<string, int> { { "a", 0 }, { "b", -20 } },
                SimpleAssembler.Interpret(new[] { "mov a -10", "mov b a", "inc a", "dec b", "jnz a -2" }));
        }
    }
}
