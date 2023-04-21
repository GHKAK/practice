
namespace TestProject1 {
    public class Tests {
        [SetUp]
        public void Setup() {
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

        }
        [TestFixture]
        public class NumberChecker {
            string odd = "нечетное";
            string even = "четное";
            string prime = "простое";
            string composite = "составное";
            [TestCase(false, 1)]

        }
    }
}