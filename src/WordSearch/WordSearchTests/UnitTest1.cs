using NUnit.Framework;

namespace WordSearchTests {
    [TestFixture]
    public class StringExtensionsTests {
        [Test]
        [TestCase("This is a test. Testing the regex functionality!", "test", 1)]
        [TestCase("This is a test. Testing the regex functionality.", "example", 0)]
        [TestCase("This is a test. Testing the regex functionality.", "", 0)]
        [TestCase("", "test", 0)]
        [TestCase("This is a test. Testing the regex functionality.", "Test", 1)]
        [TestCase("This is a test. Testing the regex functionality.", "\\b\\w{5,}\\b", 0)]
        [TestCase("This is a test. Testing the regex functionality!", "TEST", 1)]
        [TestCase("This is a test. Testing the regex functionality.", "Test!", 0)]
        [TestCase("This is a test. Testing the regex functionality.", "Test\\b", 0)]
        [TestCase("This is a test. Testing the regex functionality.", "This", 1)]
        [TestCase("This is a test. Testing the regex functionality.", "\\w", 0)]
        [TestCase("The animal [what kind?] was visible [by whom?] from the window.", "[(.*?)]", 0)]
        public void SearchCountTest(string text, string word, int expectedCount) {
            int count = text.AllMatchesCount(word);
            Assert.AreEqual(expectedCount, count, $"The count of matches for word '{word}' should be {expectedCount}.");
        }
    }
}