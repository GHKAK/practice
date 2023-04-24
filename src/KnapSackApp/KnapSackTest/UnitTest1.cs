namespace KnapSackTest {
    [TestFixture]
    public class KnapSackTests {
        private List<Thing> things;
        private List<Thing> expected;

        [SetUp]
        public void SetUp() {
            things = new List<Thing> {
            new Thing { Weight = 1, Value = 1 },
            new Thing { Weight = 2, Value = 3 },
            new Thing { Weight = 3, Value = 5 },
            new Thing { Weight = 4, Value = 7 }
        };

            expected = new List<Thing> {
            new Thing { Weight = 4, Value = 7 },
            new Thing { Weight = 3, Value = 5 }
        };
        }

        [Test]
        public void TestReturnCorrect() {
            int maxWeight = 7;

            List<Thing> result = KnapSack.KnapSackList(things, maxWeight);

            Assert.AreEqual(expected.Count, result.Count);
            for(int i = 0; i < expected.Count; i++) {
                Assert.AreEqual(expected[i].Weight, result[i].Weight);
                Assert.AreEqual(expected[i].Value, result[i].Value);
            }
        }

        [Test]
        public void TestMaxWeightZero() {
            int maxWeight = 0;

            List<Thing> result = KnapSack.KnapSackList(things, maxWeight);

            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void TestWeightBigger() {
            things = new List<Thing> {
            new Thing { Weight = 5, Value = 1 },
            new Thing { Weight = 6, Value = 3 },
            new Thing { Weight = 7, Value = 5 },
            new Thing { Weight = 8, Value = 7 }
        };
            int maxWeight = 3;
            List<Thing> result = KnapSack.KnapSackList(things, maxWeight);
            Assert.AreEqual(0, result.Count);
        }

    }
}