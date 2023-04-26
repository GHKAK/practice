using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using CollectionsBenchmark;
using Microsoft.Diagnostics.Tracing;
using System.Collections;

namespace CollectionsBenchmark {
    public class ListGeneric:List<long> {
        public ListGeneric():base() {
        }
        public void FillCollection(long length) {
            for (long i = 0; i < length; i++) {
                this.Add(i);
            }
        }
    }
    public class QueueGeneric : Queue<long> {

        public QueueGeneric() : base() { }
        public Queue<long> FillCollection(long length) {
            Queue<long> list = new Queue<long>();
            for (long i = 0; i < length; i++) {
                list.Enqueue(i);
            }
            return list;
        }
    }
}
public class Collections {
    public const long LENGTH = 1;
    public ListGeneric ListGeneric { get; set; }
    public Queue<long> QueueGeneric { get; set; }
    public SortedList<long, long> SortedListGeneric { get; set; }
    public Stack<long> StackGeneric { get; set; }
    public HashSet<long> HashSetGeneric { get; set; }
    public ArrayList ArrayList { get; set; }
    public Hashtable Hashtable { get; set; }
    public Queue Queue { get; set; }
    public Stack Stack { get; set; }
    public Collections() {
        ListGeneric = new ListGeneric();
        //QueueGeneric = (new Queue<long>());
        //SortedListGeneric = new SortedList<long, long>();
        //StackGeneric = new Stack<long>();
        //HashSetGeneric = new HashSet<long>();
        //ArrayList = new ArrayList();
        //Hashtable = new Hashtable();
        //Queue = new Queue();
        //Stack = new Stack();
    }
}
[MemoryDiagnoser]
public class AddBenchmark {
    public const long LENGTH = 1;
    private Collections _collections;
    [GlobalSetup]
    public void Setup() {
        _collections = new Collections();
    }
    [Benchmark]
    public void ListCreate() {
        _collections.ListGeneric.Add(1);
    }
    [GlobalCleanup] 
    public void Cleanup() {
        _collections = null;
    }
}