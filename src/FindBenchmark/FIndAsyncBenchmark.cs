using BenchmarkDotNet.Attributes;
using NUnit.Framework;
using PassportsTests;

namespace FindBenchmark {
    [MemoryDiagnoser]
    public class FIndAsyncBenchmark {
        public Tests Tests { get; set; }

        [GlobalSetup]
        public void SetupData() {
            Tests = new Tests();
        }

        [Benchmark]
        public void BencmarkFindAsync() {
            Tests.FoundCorrectAsyncTest();
        }
    }
}
