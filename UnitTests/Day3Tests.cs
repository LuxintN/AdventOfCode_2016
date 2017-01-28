using System.Collections.Generic;
using System.Collections;
using NUnit.Framework;
using Day3;

namespace UnitTests
{
    public class Day3Tests
    {
        private static readonly List<List<int>> input = new List<List<int>>() 
        { 
            new List<int>() {0, 0, 0}, 
            new List<int>() {1, 2, 3}, 
            new List<int>() {2, 3, 4},
            new List<int>() {3, 4, 5},
            new List<int>() {4, 5, 6},
            new List<int>() {5, 6, 7},
            new List<int>() {6, 7, 8},
            new List<int>() {7, 8, 9},
            new List<int>() {8, 9, 10}
        };

        public static IEnumerable IsPossibleTriangleTestCases
        {
            get
            {
                yield return new TestCaseData(input[0]).Returns(false);
                yield return new TestCaseData(input[1]).Returns(false);
                yield return new TestCaseData(input[2]).Returns(true);
                yield return new TestCaseData(input[3]).Returns(true);
                yield return new TestCaseData(input[4]).Returns(true);
                yield return new TestCaseData(input[5]).Returns(true);
                yield return new TestCaseData(input[6]).Returns(true);
                yield return new TestCaseData(input[7]).Returns(true);
                yield return new TestCaseData(input[8]).Returns(true);
            }
        }

        [Test, TestCaseSource("IsPossibleTriangleTestCases")]
        public bool IsPossibleTriangle_ShouldReturnCorrectValue(List<int> triangle)
        {
            return Day3Puzzles.IsPossibleTriangle(triangle);
        }

        [Test]
        public void GetPossibleTriangleCountByRows_ShouldReturnCorrectValue()
        {
            var triangleCount = Day3Puzzles.GetPossibleTriangleCountByRows(input);
            Assert.AreEqual(7, triangleCount);
        }

        [Test]
        public void GetPossibleTriangleCountByColumns_ShouldReturnCorrectValue()
        {
            var triangleCount = Day3Puzzles.GetPossibleTriangleCountByColumns(input);
            Assert.AreEqual(6, triangleCount);
        }
    }
}
