
using NumTypeConsole;

namespace TestProject1 {
    public class Tests {

        [SetUp]
        public void Setup() {
            List<string> oddPrime = new List<string>() { "Odd", "Prime" };
            List<string> evenPrime = new List<string>() { "Even", "Prime" };
            List<string> evenComposite = new List<string>() { "Even", "Composite" };
            List<string> oddComposite = new List<string>() { "Odd", "Composite" };
            List<string> odd = new List<string>() { "Odd" };
            List<string> even = new List<string>() { "Even" };
        }

        [Test]
        public void EvenNumberTest() {
            Assert.IsTrue(TypeCheck.IsEvenNumber(0));
            Assert.IsTrue(TypeCheck.IsEvenNumber(-123132));
            Assert.IsFalse(TypeCheck.IsEvenNumber(-123131));
            Assert.IsFalse(TypeCheck.IsEvenNumber(Int32.MaxValue));
            Assert.IsTrue(TypeCheck.IsEvenNumber(Int32.MinValue));
        }
        [Test]
        public void IsOddNumberTest() {
            Assert.IsFalse(TypeCheck.IsOddNumber(0));
            Assert.IsTrue(TypeCheck.IsOddNumber(-123131));
            Assert.IsFalse(TypeCheck.IsOddNumber(-123132));
            Assert.IsTrue(TypeCheck.IsOddNumber(Int32.MaxValue));
            Assert.IsFalse(TypeCheck.IsOddNumber(Int32.MinValue));
        }
        [TestFixture]
        public class IsPrimeNumberTest {
            [TestCase(false, 1)]
            [TestCase(true, 3)]
            [TestCase(true, 73939133)]
            [TestCase(false, 0)]
            [TestCase(false, -1)]
            [TestCase(false, Int32.MinValue)]
            [TestCase(true, Int32.MaxValue)]
            public void TestPrimeNumber(bool expected, int number) {
                Assert.AreEqual(expected, TypeCheck.IsPrimeNumber(number));
            }
        }
        [TestFixture]
        public class IsCompositeNumberTest {
            [TestCase(false, 1)]
            [TestCase(false, 3)]
            [TestCase(false, 73939133)]
            [TestCase(false, 0)]
            [TestCase(false, -1)]
            [TestCase(false, Int32.MinValue)]
            [TestCase(false, Int32.MaxValue)]
            public void TestCompositeNumber(bool expected, int number) {
                Assert.AreEqual(expected, TypeCheck.IsCompositeNumber(number));
            }
        }
        [TestFixture]
        public class InputParserTest {
            NumberChecker numberChecker = new NumberChecker();
            [TestCase("")]
            [TestCase("3124mgds")]
            [TestCase("542/hsf")]
            [TestCase("546/7")]
            [TestCase("546.7")]
            [TestCase("546,7")]
            [TestCase("546,")]
            [TestCase("546.")]
            [TestCase(".")]
            [TestCase(",")]
            [TestCase("asgahf")]
            public void TestNumberThrow(string number) {
                Assert.Throws<ArgumentException>(() => InputParser.ParseToInt(number));
            }
            [TestCase("214",214)]
            [TestCase("0",0)]
            [TestCase("-3124",-3124)]
            [TestCase("+4325",4325)]

            public void TestNumberEpxected(string number, int expected) {
                Assert.IsTrue(InputParser.ParseToInt(number) == expected);
            }
        }
        [TestFixture]
        public class NumberCheckerTest {
            NumberChecker numberChecker = new NumberChecker();
            [TestCase(1, "Odd")]
            [TestCase(2, "Even", "Prime")]
            [TestCase(7, "Odd", "Prime")]

            [TestCase(-15641, "Odd")]
            [TestCase(-15642, "Even")]
            [TestCase(Int32.MaxValue, "Odd", "Prime")]
            [TestCase(Int32.MaxValue - 1, "Even", "Composite")]

            public void TestNumber(int number, params string[] expected) {
                Assert.IsTrue(Enumerable.SequenceEqual(numberChecker.GetInfo(number), expected));
            }
        }
    }
}