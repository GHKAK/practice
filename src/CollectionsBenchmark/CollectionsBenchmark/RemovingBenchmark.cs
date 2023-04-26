using BenchmarkDotNet.Attributes;
using System.Collections;

namespace CollectionsBenchmark {
    [MemoryDiagnoser]
    public class RemovingBenchmark {
        private const long TEST_LENGTH = 1000;

        private List<long> _list;
        private ArrayList _arrayList;

        private LinkedList<long> _linkedList;

        private SortedList<long, long> _sortedListGeneric;
        private SortedList _sortedList;

        private Stack<long> _stackGeneric;
        private Stack _stack;

        private HashSet<long> _hashSetGeneric;

        private Hashtable _hashTable;
        public RemovingBenchmark() {
            _list = new List<long>();
            _arrayList = new ArrayList();
            _linkedList = new LinkedList<long>();
            _sortedListGeneric = new SortedList<long, long>(); 
            _sortedList = new SortedList(); 
            _hashSetGeneric = new HashSet<long>(); 
            _hashTable = new Hashtable(); 
            for (long i = 0; i < TEST_LENGTH; i++) {
                _list.Add(i);
                _arrayList.Add(i);
                _linkedList.AddLast(0);
                _sortedListGeneric.Add(i, i);
                _sortedList.Add(i, i);
                _hashSetGeneric.Add(i);
                _hashTable.Add(i, i);
            }
        }
        public void DoActionLengthTimes(Action<long> action) {
            for (int i = 0; i < TEST_LENGTH; i++) {
                action(i);
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
            DoActionLengthTimes((long x) => _hashSetGeneric.Remove(x));
        }
        [Benchmark]
        public void HashTableRemove() {
            DoActionLengthTimes((long x) => _hashTable.Remove(x));
        }
    }
}
