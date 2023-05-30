using BenchmarkDotNet.Attributes;
using NUnit.Framework;
using PassportsTests;
using System.CodeDom;

namespace FindBenchmark {
    [MemoryDiagnoser]
    public class FindAsyncBenchmark {
        public Tests Tests { get; set; }
        public FindAsyncBenchmark() {
            Tests = new Tests();
        }

        [GlobalSetup]
        public void SetupData() {
            Tests = new Tests();
        }
        [Benchmark]
        public async Task<int> BenchmarkFindInMemoryAsync() {
            //await Tests.FoundInMemoryCorrect();
            return 1;
        }
        //[Benchmark]
        //public async Task<int> BenchmarkFindAsync() {
        //    await Tests.FoundCorrectAsyncTest();
        //    return 1;
        //}
    }
}
