using System.Collections.Generic;
using Day10;
using NUnit.Framework;


namespace UnitTests
{
    public class Day10Tests
    {
        [Test]
        public void GetBotResponsibleForComparingValues_ReturnsCorrectBotNumber()
        {
            // Arrange
            var input = new List<string>()
            {
                "value 5 goes to bot 2",
                "bot 2 gives low to bot 1 and high to bot 0",
                "value 3 goes to bot 1",
                "bot 1 gives low to output 1 and high to bot 0",
                "bot 0 gives low to output 2 and high to output 0",
                "value 2 goes to bot 2"
            };
            var expectedBotNumber = 2;

            // Act
            var botNumber = Day10Puzzles.GetBotResponsibleForComparingValues(input, 5, 2, false);

            // Assert
            Assert.AreEqual(expectedBotNumber, botNumber);
        }

    }
}
