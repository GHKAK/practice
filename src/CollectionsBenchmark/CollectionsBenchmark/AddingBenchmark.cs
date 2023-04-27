using BenchmarkDotNet.Attributes;
using System.Collections;

namespace CollectionsBenchmark {
    [MemoryDiagnoser]
    public class AddingBenchmark : MethodsBenchmark {
        public virtual void DoActionLengthTimes(Action<long, long> action) {
            for (int i = 0; i < TEST_LENGTH; i++) {
                long descend = TEST_LENGTH - i;
                action(descend, descend);
            }
        }
        [Benchmark]
        public void ListAdd() {
            _list = new List<long>();
            DoActionLengthTimes(() => _list.Add(0));
        }
        [Benchmark]
        public void ListAdd1() {
            //_list = new List<long>();
            DoActionLengthTimes(() => { 
                var l = new List<long>();
                l.Add(0);
            });
        }
        [Benchmark]
        public void ArrayListAdd() {
            _arrayList = new ArrayList();
            DoActionLengthTimes(() => _arrayList.Add(0));
        }
        [Benchmark]
        public void DictionaryAdd() {
            _dictionary = new Dictionary<long, long>();
            DoActionLengthTimes((long key, long value) => _dictionary.Add(key, value));
        }
        [Benchmark]
        public void HashTableAdd() {
            _hashTable = new Hashtable();
            DoActionLengthTimes((long key, long value) => _hashTable.Add(key, value));
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
        public void SortedDictionaryGenericAdd() {
            _sortedDictionary = new SortedDictionary<long, long>();
            DoActionLengthTimes((long key, long value) => _sortedDictionary.Add(key, value));
        }
        [Benchmark]
        public void SortedListGenericAdd() {
            _sortedListGeneric = new SortedList<long, long>();
            DoActionLengthTimes((long key, long value) => _sortedListGeneric.Add(key, value));
        }
        [Benchmark]
        public void SortedListAdd() {
            _sortedList = new SortedList();
            DoActionLengthTimes((long key, long value) => _sortedList.Add(key, value));

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
            _hashSet = new HashSet<long>();
            DoActionLengthTimes((long i) => _hashSet.Add(i));
        }
    }
}
//HashSetDictionary
//add not long but class x , y;