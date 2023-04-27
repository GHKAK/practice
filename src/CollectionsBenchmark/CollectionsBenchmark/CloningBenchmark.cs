using BenchmarkDotNet.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsBenchmark {
    [MemoryDiagnoser]
    public class CloningBenchmark : MethodsBenchmark {
        [Benchmark]
        public void ListClone() {
            List<long> newCollection = new List<long>(_list);
        }
        [Benchmark]
        public void ArrayListClone() {
            ArrayList newCollection = new ArrayList(_arrayList);
        }
        [Benchmark]
        public void DictionaryClone() {
            Dictionary<long, long> newCollection = new Dictionary<long, long>(_dictionary);
        }
        [Benchmark]
        public void HashtableClone() {
            Hashtable newCollection = new Hashtable(_hashTable);
        }
        [Benchmark]
        public void LinkedListClone() {
            LinkedList<long> newCollection = new LinkedList<long>(_linkedList);
        }
        [Benchmark]
        public void QueueGenericClone() {
            Queue<long> newCollection = new Queue<long>(_queueGeneric);
        }
        [Benchmark]
        public void QueueClone() {
            Queue newCollection = new Queue(_queue);
        }
        [Benchmark]
        public void SortedDictionaryClone() {
            SortedDictionary<long, long> newCollection = new SortedDictionary<long, long>(_sortedDictionary);
        }
        [Benchmark]
        public void SortedListGenericClone() {
            SortedList<long, long> newCollection = new SortedList<long, long>(_sortedListGeneric);
        }
        [Benchmark]
        public void SortedSetClone() {
            SortedSet<long> newCollection = new SortedSet<long>(_sortedSet);
        }
        [Benchmark]
        public void SortedListClone() {
            SortedList newCollection = new SortedList(_sortedList);
        }
        [Benchmark]
        public void StackGenericClone() {
            Stack<long> newCollection = new Stack<long>(_stackGeneric);
        }
        [Benchmark]
        public void StackClone() {
            Stack newCollection = new Stack(_stack);
        }
        [Benchmark]
        public void HasSetClone() {
            HashSet<long> newCollection = new HashSet<long>(_hashSetGeneric);
        }
    }
}
