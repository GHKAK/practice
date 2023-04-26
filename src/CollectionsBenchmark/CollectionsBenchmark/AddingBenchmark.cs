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
            //DoActionLengthTimes(() => _list.Add(0));
        }
        [Benchmark]
        public void ArrayListAdd() {
            _arrayList = new ArrayList();
            //DoActionLengthTimes(() => _arrayList.Add(0));

            for (long i = 0; i < TEST_LENGTH; i++) {
                _arrayList.Add(i);
            }
        }
        [Benchmark]
        public void LinkedListAddLast() {
            _linkedList = new LinkedList<long>();
            //DoActionLengthTimes(() => _linkedList.AddLast(0));

            for (long i = 0; i < TEST_LENGTH; i++) {
                _linkedList.AddLast(i);
            }
        }
        [Benchmark]
        public void QueueGenericEnqueue() {
            _queueGeneric = new Queue<long>();
            for (long i = 0; i < TEST_LENGTH; i++) {
                _queueGeneric.Enqueue(i);
            }
        }
        [Benchmark]
        public void QueueEnqueue() {
            _queue = new Queue();
            for (long i = 0; i < TEST_LENGTH; i++) {
                _queue.Enqueue(i);
            }
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
            for (long i = 0; i < TEST_LENGTH; i++) {
                _stackGeneric.Push(i);
            }
        }
        [Benchmark]
        public void StackPush() {
            _stack = new Stack();
            for (long i = 0; i < TEST_LENGTH; i++) {
                _stackGeneric.Push(i);
            }
        }
        [Benchmark]
        public void HashSetGeneric() {
            _hashSetGeneric = new HashSet<long>();
            for (long i = 0; i < TEST_LENGTH; i++) {
                _hashSetGeneric.Add(i);
            }
        }
        [Benchmark]
        public void HashTable() {
            _hashTable = new Hashtable();
            for (long i = 0; i < TEST_LENGTH; i++) {
                _hashTable.Add(i, i);
            }
        }
    }
}
