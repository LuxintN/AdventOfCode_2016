using System;
using Day6;
using NUnit.Framework;

namespace UnitTests
{
    public class Day6Tests
    {
        private const string inputFromExample =  @" eedadn
                                                    drvtee
                                                    eandsr
                                                    raavrd
                                                    atevrs
                                                    tsrnev
                                                    sdttsa
                                                    rasrtv
                                                    nssdts
                                                    ntnada
                                                    svetve
                                                    tesnvt
                                                    vntsnd
                                                    vrdear
                                                    dvrsen
                                                    enarar";
        [Test]
        [TestCase("abc","abc")]
        [TestCase("abc \n ade \n bdc", "adc")]
        [TestCase(inputFromExample, "easter")]
        public void GetMessage_WhenConfiguredForMostCommonCharacters_MessageConsistsOfTheMostCommonCharactersInEachColumn(string input, string expectedResult)
        {
            var message = Day6Puzzles.GetMessage(input, true);
            Assert.AreEqual(expectedResult, message);
        }

        [Test]
        [TestCase("abc", "abc")]
        [TestCase("abc \n ade \n bdc", "bbe")]
        [TestCase(inputFromExample, "advent")]
        public void GetMessage_WhenConfiguredForLeastCommonCharacters_MessageConsistsOfTheLeastCommonCharactersInEachColumn(string input, string expectedResult)
        {
            var message = Day6Puzzles.GetMessage(input, false);
            Assert.AreEqual(expectedResult, message);
        }

        [Test]
        [TestCase("")]
        [TestCase("abc\nde")]
        public void GetMessage_WhenInputFormatIsInvalid_ThrowsArgumentException(string input)
        {
            Assert.Throws<ArgumentException>(() => Day6Puzzles.GetMessage(input, true));
        }   
    }
}
