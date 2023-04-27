using BenchmarkDotNet.Attributes;


namespace CollectionsBenchmark {
    [MemoryDiagnoser]
    public class SortingBenchmark : MethodsBenchmark {

        [Benchmark]
        public void ListSort() {
            _list.Sort();
        }
        [Benchmark]
        public void ArrayListSort() {
            _arrayList.Sort();
        }
    }
}
