using BenchmarkDotNet.Attributes;
using System.Collections;
using System.Collections.Generic;


namespace CollectionsBenchmark {
    [MemoryDiagnoser]
    public class SortingBenchmark {
        private const long TEST_LENGTH = 100;

        private List<long> _list;
        private ArrayList _arrayList;
        public SortingBenchmark() {
            _list = new List<long>();
            _arrayList = new ArrayList();
            for (long i = 0; i < TEST_LENGTH; i++) {
                _list.Add(i);
                _arrayList.Add(i);
            }
        }
        public void DoActionLengthTimes(Action<long> action) {
            for (int i = 0; i < TEST_LENGTH; i++) {
                action(i);
            }
        }
        [Benchmark]
        public void ListSort() {
            DoActionLengthTimes((long x) => _list.Sort());
        }
        [Benchmark]
        public void ArrayListSort() {
            DoActionLengthTimes((long x) => _arrayList.Sort());
        }
    }
}
