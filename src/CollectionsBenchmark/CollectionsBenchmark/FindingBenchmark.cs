using BenchmarkDotNet.Attributes;

namespace CollectionsBenchmark {
    [MemoryDiagnoser]
    public class FindingBenchmark : MethodsBenchmark {
        public override void DoActionLengthTimes(Action action) {
            for(int i = 0; i < TEST_LENGTH; i++) {
                action();
            }
        }
        [Benchmark]
        public void ListIndexOf() {
            DoActionLengthTimes(() => _list.IndexOf(TEST_LENGTH - 2));
        }
        [Benchmark]
        public void ArrayListIndexOf() {
            DoActionLengthTimes(() => _arrayList.IndexOf(TEST_LENGTH - 2));
        }
        [Benchmark]
        public void LinkedListFind() {
            DoActionLengthTimes(() => _linkedList.Find(TEST_LENGTH - 2));
        }
        [Benchmark]
        public void DictionaryGetValueOrDefault() {
            DoActionLengthTimes(() => _dictionary.GetValueOrDefault(TEST_LENGTH - 2));
        }
        [Benchmark]
        public void SortedListGenericIndexOfKey() {
            DoActionLengthTimes(() => _sortedListGeneric.IndexOfKey(TEST_LENGTH - 2));
        }
        [Benchmark]
        public void SortedListIndexOfKey() {
            DoActionLengthTimes(() => _sortedList.IndexOfKey(TEST_LENGTH - 2));
        }
        [Benchmark]
        public void SortedListGenericIndexOfValue() {
            DoActionLengthTimes(() => _sortedListGeneric.IndexOfValue(TEST_LENGTH - 2));
        }
        [Benchmark]
        public void SortedListIndexOfValue() {
            DoActionLengthTimes(() => _sortedList.IndexOfValue(TEST_LENGTH - 2));
        }
    }
}
