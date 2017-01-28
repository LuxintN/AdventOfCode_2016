using NUnit.Framework;
using Day2;

namespace UnitTests
{
    public class Day2Tests
    {
        string[] instructions = new[]
        {
            "ULL",
            "RRDDD",
            "LURDL",
            "UUUUD"
        };

        [Test]
        public void GetCode_UsingFirstKeypad_ShouldReturnCorrectCode()
        {
            var code = Day2Puzzles.GetCode(instructions, Day2Puzzles.Keypads[0]);
            Assert.AreEqual("1985", code);
        }

        [Test]
        public void GetCode_UsingSecondKeypad_ShouldReturnCorrectCode()
        {
            var code = Day2Puzzles.GetCode(instructions, Day2Puzzles.Keypads[1]);
            Assert.AreEqual("5DB3", code);
        }
    }
}
