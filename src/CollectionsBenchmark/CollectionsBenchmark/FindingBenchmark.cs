using BenchmarkDotNet.Attributes;
using System.Collections;

namespace CollectionsBenchmark {
    [MemoryDiagnoser]
    public class FindingBenchmark {
        private const long TEST_LENGTH = 1000;

        private List<long> _list;
        private ArrayList _arrayList;

        private LinkedList<long> _linkedList;

        private SortedList<long, long> _sortedListGeneric;
        private SortedList _sortedList;

        public void DoActionLengthTimes(Action action) {
            for (int i = 0; i < TEST_LENGTH; i++) {
                action();
            }
        }
        [Benchmark]
        public void ListIndexOf() {
            _list = new List<long>();

            DoActionLengthTimes(() => _list.IndexOf(TEST_LENGTH-2));
        }
        [Benchmark]
        public void ArrayListIndexOf() {
            _arrayList = new ArrayList();
            DoActionLengthTimes(() => _arrayList.IndexOf(TEST_LENGTH - 2));
        }
        [Benchmark]
        public void LinkedListFind() {
            _linkedList = new LinkedList<long>();
            DoActionLengthTimes(() => _linkedList.Find(TEST_LENGTH - 2));
        }
        [Benchmark]
        public void SortedListGenericIndexOfKey() {
            _sortedListGeneric = new SortedList<long, long>();
            for (long i = 0; i < TEST_LENGTH; i++) {
                _sortedListGeneric.IndexOfKey(TEST_LENGTH-2);
            }
        }
        [Benchmark]
        public void SortedListIndexOfKey() {
            _sortedList = new SortedList();
            for (long i = 0; i < TEST_LENGTH; i++) {
                _sortedList.IndexOfKey(TEST_LENGTH - 2);
            }
        }
        public void SortedListGenericIndexOfValue() {
            _sortedListGeneric = new SortedList<long, long>();
            for(long i = 0; i < TEST_LENGTH; i++) {
                _sortedListGeneric.IndexOfValue(TEST_LENGTH - 2);
            }
        }
        [Benchmark]
        public void SortedListIndexOfValue() {
            _sortedList = new SortedList();
            for(long i = 0; i < TEST_LENGTH; i++) {
                _sortedList.IndexOfValue(TEST_LENGTH - 2);
            }
        }
    }
}
