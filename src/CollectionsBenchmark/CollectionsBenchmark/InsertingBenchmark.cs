using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using System.Collections;
using System.Collections.Generic;

namespace CollectionsBenchmark {
    [MemoryDiagnoser]
    [MinIterationCount(1)]
    [WarmupCount(3)]
    [MaxIterationCount(5)]
    public class InsertingBenchmark : MethodsBenchmark {
        public override void DoActionLengthTimes(Action<int, long> action) {
            for(int i = 1000; i < TEST_LENGTH * 2; i++) {
                action(i, i);
            }
        }
        public override void DoActionLengthTimes(Action<long> action) {
            for(int i = 1000; i < TEST_LENGTH * 2; i++) {
                long x = TEST_LENGTH * 2 - i;
                action(i);
            }
        }
        [Benchmark]
        public void ListInsert() {
            DoActionLengthTimes((int pos, long x) => _list.Insert(pos, x));
        }
        [Benchmark]
        public void ArrayListInsert() {
            DoActionLengthTimes((int pos, long x) => _arrayList.Insert(pos, x));
        }
        [Benchmark]
        public void DictionaryAdd() {
            _dictionary = new Dictionary<long, long>();
            DoActionLengthTimes((int key, long x) => _dictionary.Add(key, x));
        }
        [Benchmark]
        public void SortedDictionaryAdd() {
            _sortedDictionary = new SortedDictionary<long, long>();
            DoActionLengthTimes((int key, long x) => _sortedDictionary.Add(key, x));
        }
        [Benchmark]
        public void SortedSetAdd() {
            DoActionLengthTimes((long x) => _sortedSet.Add(x));
        }
    }
}
