using Day7;
using NUnit.Framework;

namespace UnitTests
{
    public class Day7Tests
    {
        [Test]
        [TestCase("abba[mnop]qrst",true)]
        [TestCase("abcd[bddb]xyyx", false)]
        [TestCase("aaaa[qwer]tyui", false)]
        [TestCase("ioxxoj[asdfgh]zxcvbn", true)]
        public void CheckIfSupportsTls_ReturnsCorrectValue(string input, bool expectedValue)
        {
            bool result = Day7Puzzles.CheckIfSupportsTls(input);
            Assert.AreEqual(expectedValue, result);
        }

        [Test]
        [TestCase("aba[bab]xyz", true)]
        [TestCase("xyx[xyx]xyx", false)]
        [TestCase("aaa[kek]eke", true)]
        [TestCase("zazbz[bzb]cdb", true)]
        public void CheckIfSupportsSsl_ReturnsCorrectValue(string input, bool expectedValue)
        {
            bool result = Day7Puzzles.CheckIfSupportsSsl(input);
            Assert.AreEqual(expectedValue, result);
        }
    }
}
