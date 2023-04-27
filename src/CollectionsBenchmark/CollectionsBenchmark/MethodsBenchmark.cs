using BenchmarkDotNet.Attributes;
using System.Collections;

namespace CollectionsBenchmark {
    public class MethodsBenchmark {
        private protected const long TEST_LENGTH = 1000;
        private protected const long INSERT_COUNT = 1;
        private protected const int INSERT_POS = 2;
        private protected List<long> _list;
        private protected ArrayList _arrayList;

        private protected Dictionary<long, long> _dictionary;
        private protected Hashtable _hashTable;

        private protected LinkedList<long> _linkedList;

        private protected Queue<long> _queueGeneric;
        private protected Queue _queue;

        private protected SortedDictionary<long, long> _sortedDictionary;
        private protected SortedList<long, long> _sortedListGeneric;
        private protected SortedSet<long> _sortedSet;
        private protected SortedList _sortedList;

        private protected Stack<long> _stackGeneric;
        private protected Stack _stack;

        private protected HashSet<long> _hashSetGeneric;
        [GlobalSetup]
        public void Setup() {
            _list = new List<long>();
            _arrayList = new ArrayList();

            _dictionary = new Dictionary<long, long>();
            _hashTable = new Hashtable();

            _linkedList = new LinkedList<long>();

            _queueGeneric = new Queue<long>();
            _queue = new Queue();

            _sortedDictionary = new SortedDictionary<long, long>();
            _sortedListGeneric = new SortedList<long, long>();
            _sortedSet = new SortedSet<long>();
            _sortedList = new SortedList();

            _stackGeneric = new Stack<long>();
            _stack = new Stack();

            _hashSetGeneric = new HashSet<long>();
            for(long i = 0; i < TEST_LENGTH; i++) {
                long elem = i;
                _list.Add(elem);
                _arrayList.Add(elem);

                _dictionary.Add(elem, elem);
                _hashTable.Add(elem, elem);

                _linkedList.AddLast(elem);
                _sortedDictionary.Add(elem, elem);
                _sortedListGeneric.Add(elem, elem);
                _sortedList.Add(elem, elem);
                _sortedSet.Add(i);
                _queueGeneric.Enqueue(elem);
                _queue.Enqueue(elem);

                _stackGeneric.Push(elem);
                _stack.Push(elem);

                _hashSetGeneric.Add(elem);
            }
        }
        public virtual void DoActionLengthTimes(Action action) {
            for(int i = 0; i < TEST_LENGTH; i++) {
                action();
            }
        }
        public virtual void DoActionLengthTimes(Action<long> action) {
            for(int i = 0; i < TEST_LENGTH; i++) {
                action(i);
            }
        }
        public virtual void DoActionLengthTimes(Action<int, long> action) {
            for(int i = 0; i < TEST_LENGTH; i++) {
                action(INSERT_POS, i);
            }
        }
        public virtual void DoActionLengthTimes(Action<long, long> action) {
            for(int i = 0; i < TEST_LENGTH; i++) {
                action(i, i);
            }
        }
    }
}
