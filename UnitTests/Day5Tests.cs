using NUnit.Framework;
using Day5;

namespace UnitTests
{
    public class Day5Tests
    {
        [Test]
        public void GetPasswords_ShouldReturnCorrectPasswords()
        {
            var input = "abc";

            string password1, password2;
            Day5Puzzles.GetPasswords(input, out password1, out password2);

            Assert.AreEqual("18f47a30", password1);
            Assert.AreEqual("05ace8e3", password2);
        }
    }
}
