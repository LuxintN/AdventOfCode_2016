using System;
using NUnit.Framework;
using Day1;

namespace UnitTests
{
    public class Day1Tests
    {
        [Test]
        [TestCase("R2, L3", 5)]
        [TestCase("R2, R2, R2", 2)]
        [TestCase("R5, L5, R5, R3", 12)]
        public void FollowIntructions_ShouldReturnCorrectTotalBlockCount(string instructionString, int totalBlockCountExpectedValue) 
        {
            Direction currentDirection = Direction.North;
            int northBlockCount = 0, eastBlockCount = 0;

            var totalBlockCount = Day1Puzzles.FollowInstructions(instructionString, currentDirection, ref northBlockCount, ref eastBlockCount);

            Assert.AreEqual(totalBlockCountExpectedValue, totalBlockCount);
        }

        [Test]
        [TestCase("R3, L2, R4, L2, L3, L5", 6, 2, 4)]
        [TestCase("L2, R2, L4, L3, L2, L7", 6, 2, -4)]
        [TestCase("L4, L3, L1, L1, L4", 6, -2, -4)]
        [TestCase("L2, L2, L3, R2, R6, R3, R5", 3, -1, -2)]
        public void FollowIntructions_ShouldFindCorrectFirstLocationVisitedTwice(string instructionString, int totalBlockCountExpectedValue, int northBlockCountExpectedValue, int eastBlockCountExpectedValue)
        {
            Direction currentDirection = Direction.North;
            int northBlockCount = 0, eastBlockCount = 0;

            Day1Puzzles.FollowInstructions(instructionString, currentDirection, ref northBlockCount, ref eastBlockCount);

            Assert.AreEqual(northBlockCountExpectedValue, Day1Puzzles.FirstLocationVisitedTwice.North);
            Assert.AreEqual(eastBlockCountExpectedValue, Day1Puzzles.FirstLocationVisitedTwice.East);
            Assert.AreEqual(totalBlockCountExpectedValue, 
                Math.Abs(Day1Puzzles.FirstLocationVisitedTwice.North) + Math.Abs(Day1Puzzles.FirstLocationVisitedTwice.East));
        }
    }
}
