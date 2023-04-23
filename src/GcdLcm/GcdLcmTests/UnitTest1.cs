using GcdLcm;
using GcdLcmLib;

namespace GcdLcmTests {
    [TestFixture]
    public class ConsoleInputTests {
        [TestCase("1", 1)]
        [TestCase("-5", -5)]
        [TestCase("0", 0)]
        [TestCase("+4", 4)]
        public void GetNumberTest(string input, int expected) {
            int result;
            using(StringReader stringReader = new StringReader(input)) {
                Console.SetIn(stringReader);
                ConsoleInput.GetNumber(out result);
            }
            Assert.AreEqual(expected, result);
        }
        [TestCase(new string[] { "abc", "456", "456" }, 456, 456)]
        [TestCase(new string[] { "abc", "456", "abc", "456" }, 456, 456)]
        [TestCase(new string[] { "abc", "456", "abc", "abc", "456" }, 456, 456)]
        [TestCase(new string[] { "456", "456" }, 456, 456)]
        public void GetInvalidTest(string[] inputs, params int[] expectedValues) {
            int expected = 456;
            int result;
            using(StringReader stringReader = new StringReader(string.Join(Environment.NewLine, inputs))) {
                Console.SetIn(stringReader);
                ConsoleInput.GetNumber(out result);
            }
            Assert.AreEqual(expected, result);
        }
    }
    [TestFixture]
    public class GcdLcmAlgorithmsTests {
        [TestCase(0, 0, 0)]
        [TestCase(0, 5, 5)]
        [TestCase(5, 0, 5)]
        [TestCase(3, 5, 1)]
        [TestCase(12, 18, 6)]
        [TestCase(-12, 18, 6)]
        [TestCase(12, -18, 6)]
        [TestCase(-12, -18, 6)]
        [TestCase(17, 23, 1)]
        public void CalculateGcdTest(int x, int y, int expectedGcd) {
            int result = GcdLcmAlgorithms.CalculateGcd(x, y);
            Assert.AreEqual(expectedGcd, result);
        }

        [TestCase(0, 0, 0)]
        [TestCase(0, 5, 0)]
        [TestCase(5, 0, 0)]
        [TestCase(3, 5, 15)]
        [TestCase(12, 18, 36)]
        [TestCase(-12, 18, 36)]
        [TestCase(12, -18, 36)]
        [TestCase(-12, -18, 36)]
        [TestCase(17, 23, 391)]
        public void CalculateLcmTest(int x, int y, int expectedLcm) {
            int result = GcdLcmAlgorithms.CalculateLcm(x, y);
            Assert.AreEqual(expectedLcm, result);
        }
    }
}