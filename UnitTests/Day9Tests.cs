using Day9;
using NUnit.Framework;

namespace UnitTests
{
    public class Day9Tests
    {
        [Test]
        [TestCase("ADVENT", "ADVENT")]
        [TestCase("A(1x5)BC", "ABBBBBC")]
        [TestCase("(3x3)XYZ", "XYZXYZXYZ")]
        [TestCase("A(2x2)BCD(2x2)EFG", "ABCBCDEFEFG")]
        [TestCase("(6x1)(1x3)A", "(1x3)A")]
        [TestCase("X(8x2)(3x3)ABCY", "X(3x3)ABC(3x3)ABCY")]
        public void GetDecompressedStringLength_RecursionIsDisabled_ReturnsCorrectLengthAndString(string input, string expectedDecompressedString)
        {
            // Arrange
            string decompressedString;

            // Act
            var decompressedStringLength = Day9Puzzles.GetDecompressedStringLength(input, false, out decompressedString);

            // Assert
            Assert.AreEqual(expectedDecompressedString, decompressedString);
            Assert.AreEqual(expectedDecompressedString.Length, decompressedStringLength);
        }

        [Test]
        [TestCase("ADVENT", 6)]
        [TestCase("(3x3)XYZ", 9)]
        [TestCase("X(8x2)(3x3)ABCY", 1+2*(3*3)+1)]
        [TestCase("(27x12)(20x12)(13x14)(7x10)(1x12)A", 241920)]
        [TestCase("(25x3)(3x3)ABC(2x3)XY(5x2)PQRSTX(18x9)(3x2)TWO(5x7)SEVEN", 445)]

        public void GetDecompressedStringLength_RecursionIsEnabled_ReturnsCorrectLength(string input, int expectedDecompressedStringLength)
        {
            // Arrange
            string decompressedString;

            // Act
            var decompressedStringLength = Day9Puzzles.GetDecompressedStringLength(input, true, out decompressedString);

            // Assert
            Assert.AreEqual(decompressedString, null);
            Assert.AreEqual(expectedDecompressedStringLength, decompressedStringLength);
        }
    }
}
