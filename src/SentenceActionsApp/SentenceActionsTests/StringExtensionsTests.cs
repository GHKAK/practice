using NUnit.Framework;
using SentenceActionsApp;

namespace SentenceActionsTests {
    [TestFixture]
    public class StringExtensionsTests {
        [Test]
        [TestCase("This is a test sentence.", 5)]
        [TestCase("Count     words    in   this  sentence!", 5)]
        [TestCase("A  single  word.", 3)]
        [TestCase("", 0)]
        [TestCase("afds afio 925 gds  risoe sgdf ",6)]
        public void CountWordsTest(string sentence, int expectedWordCount) {
            int wordCount = sentence.CountWords();
            Assert.AreEqual(expectedWordCount, wordCount);
        }

        [Test]
        [TestCase("this is a test sentence.", "This Is A Test Sentence.")]
        [TestCase("first   letters  to  uppercase!", "First   Letters  To  Uppercase!")]
        [TestCase("a  single  word.", "A  Single  Word.")]
        [TestCase("ssdf s 58348tg  ", "Ssdf S 58348Tg  ")]
        public void FirstLetterToUpperCaseTest(string sentence, string expected) {
            string result = sentence.FirstLettersToUpperCase();
            Assert.AreEqual(expected, result);
        }

        [Test]
        [TestCase("This is a test sentence.", "a is sentence test This")]
        [TestCase("Sort   this   sentence   alphabetically!", "alphabetically sentence Sort this")]
        [TestCase("A  single  word.", "A single word")]
        public void SortSentenceTest(string sentence, string expected) {
            string result = sentence.SortSentence();
            Assert.AreEqual(expected, result);
        }
    }
}