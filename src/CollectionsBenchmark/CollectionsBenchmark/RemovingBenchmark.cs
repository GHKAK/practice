using BenchmarkDotNet.Attributes;

namespace CollectionsBenchmark {
    [MemoryDiagnoser]
    public class RemovingBenchmark : MethodsBenchmark {
        public override void DoActionLengthTimes(Action<long> action) {
            for (int i = 0; i < TEST_LENGTH; i++) {
                action(TEST_LENGTH);
            }
        }
        [Benchmark]
        public void ListRemove() {
            DoActionLengthTimes((long x) => _list.Remove(x));
        }
        [Benchmark]
        public void ArrayListRemove() {
            DoActionLengthTimes((long x) => _arrayList.Remove(x));
        }
        [Benchmark]
        public void LinkedListRemove() {
            DoActionLengthTimes((long x) => _linkedList.Remove(x));
        }
        [Benchmark]
        public void SortedListGenericRemove() {
            DoActionLengthTimes((long x) => _sortedListGeneric.Remove(x));
        }
        [Benchmark]
        public void SortedListRemove() {
            DoActionLengthTimes((long x) => _sortedList.Remove(x));
        }
        [Benchmark]
        public void HashSetGenericRemove() {
            DoActionLengthTimes((long x) => _hashSet.Remove(x));
        }
        [Benchmark]
        public void HashTableRemove() {
            DoActionLengthTimes((long x) => _hashTable.Remove(x));
        }
    }
}
