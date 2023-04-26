using BenchmarkDotNet.Attributes;
using System.Collections;
namespace CollectionsBenchmark {
    [MemoryDiagnoser]
    public class AddingBencmark {
        private const long TEST_LENGTH = 1000;

        private List<long> _list;
        private ArrayList _arrayList;

        private LinkedList<long> _linkedList;

        private Queue<long> _queueGeneric;
        private Queue _queue;

        private SortedList<long, long> _sortedListGeneric;
        private SortedList _sortedList;

        public Stack<long> _stackGeneric;
        public Stack _stack;

        public HashSet<long> _hashSetGeneric;

        public Hashtable _hashTable;

        public void DoActionLengthTimes(Action action) {
            for (int i = 0; i < TEST_LENGTH; i++) {
                action();
            }
        }
        [Benchmark]
        public void ListAdd() {
            _list = new List<long>();
            DoActionLengthTimes(() => _list.Add(0));
        }
        [Benchmark]
        public void ArrayListAdd() {
            _arrayList = new ArrayList();
            DoActionLengthTimes(() => _arrayList.Add(0));
        }
        [Benchmark]
        public void LinkedListAddLast() {
            _linkedList = new LinkedList<long>();
            DoActionLengthTimes(() => _linkedList.AddLast(0));
        }
        [Benchmark]
        public void QueueGenericEnqueue() {
            _queueGeneric = new Queue<long>();
            DoActionLengthTimes(() => _queueGeneric.Enqueue(0));
        }
        [Benchmark]
        public void QueueEnqueue() {
            _queue = new Queue();
            DoActionLengthTimes(() => _queue.Enqueue(0));
        }
        [Benchmark]
        public void SortedListGenericAdd() {
            _sortedListGeneric = new SortedList<long, long>();
            for (long i = 0; i < TEST_LENGTH; i++) {
                _sortedListGeneric.Add(i, i);
            }
        }
        [Benchmark]
        public void SortedListAdd() {
            _sortedList = new SortedList();
            for (long i = 0; i < TEST_LENGTH; i++) {
                _sortedList.Add(i, i);
            }
        }
        [Benchmark]
        public void StackGenericPush() {
            _stackGeneric = new Stack<long>();
            DoActionLengthTimes(() => _stackGeneric.Push(0));
        }
        [Benchmark]
        public void StackPush() {
            _stack = new Stack();
            DoActionLengthTimes(() => _stack.Push(0));
        }
        [Benchmark]
        public void HashSetGenericAdd() {
            _hashSetGeneric = new HashSet<long>();
            DoActionLengthTimes(() => _hashSetGeneric.Add(0));
        }
        [Benchmark]
        public void HashTableAdd() {
            _hashTable = new Hashtable();
            for (long i = 0; i < TEST_LENGTH; i++) {
                _hashTable.Add(i, i);
            }
        }
    }
}