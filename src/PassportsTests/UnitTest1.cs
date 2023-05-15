using Passports.Repositories;
namespace PassportsTests {
    public class Tests {
        private LocalRepository _localRepository;
        public Tests()
        {
            _localRepository = new();
        }
        [SetUp]
        public void Setup() {
            _localRepository = new();
        }

        [Test]
        public async Task FoundCorrectAsyncTest() {
            int actualMatches = await _localRepository.FindInChunksAsync(9201, 335501);
            int expectedMatches = 1;

            Assert.AreEqual(actualMatches, expectedMatches);
        }
        [Test]
        public async Task NotFoundAsyncTest() {
            int actualMatches = await _localRepository.FindInChunksAsync(05145, 000);
            int expectedMatches = 0;

            Assert.AreEqual(actualMatches, expectedMatches);
        }
    }
}