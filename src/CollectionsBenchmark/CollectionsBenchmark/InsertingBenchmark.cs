using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using System.Collections;
using System.Collections.Generic;

namespace CollectionsBenchmark {
    [MemoryDiagnoser]
    public class InsertingBenchmark {
        private const long TEST_LENGTH = 10;
        private const long INSERT_COUNT = 1;
        private const int INSERT_POS = 2;
        private List<long> _list;
        private ArrayList _arrayList;

        private LinkedList<long> _linkedList;


        public InsertingBenchmark() {
            _list = new List<long>();
            _arrayList = new ArrayList();
            _linkedList = new LinkedList<long>();
            for (long i = 0; i < TEST_LENGTH; i++) {
                _list.Add(i);
                _arrayList.Add(i);
                _linkedList.AddLast(0);
            }
        }
        public void DoActionLengthTimes(Action<int, long> action) {
            for (int i = 0; i < INSERT_COUNT; i++) {
                action(INSERT_POS ,i);
            }
        }
        [Benchmark]
        [MinIterationCount(1)]
        [WarmupCount(3)]
        [MaxIterationCount(5)]
        public void ListInsert() {
            DoActionLengthTimes((int pos, long x) => _list.Insert(pos, x));
        }
        [Benchmark]
        [WarmupCount(3)]
        [MinIterationCount(1)]
        [MaxIterationCount(5)]
        public void ArrayListInsert() {
            DoActionLengthTimes((int pos, long x) => _arrayList.Insert(pos, x));
        }
    }
}
