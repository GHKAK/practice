using BenchmarkDotNet.Attributes;
using NUnit.Framework;
using PassportsTests;
using System.CodeDom;

namespace FindBenchmark {
    [MemoryDiagnoser]
    public class FIndAsyncBenchmark {
        public Tests Tests { get; set; }
        public FIndAsyncBenchmark() {
            Tests = new Tests();
        }

        [GlobalSetup]
        public void SetupData() {
            Tests = new Tests();
        }

        [Benchmark]
        public async Task<int> BenchmarkFindAsync() {
            await Tests.FoundCorrectAsyncTest();
            return 1;
        }
    }
}
