//using BenchmarkDotNet.Attributes;
//using BenchmarkDotNet.Running;

//namespace CollectionsBenchmark {
//    public interface IBench<T> {
//        public T Collection { get; set; }
//        public void GenerateCollection();
//    }
//    public class BenchListLong : IBench<List<long>> {
//        public List<long> Collection { get; set; }
//        public BenchListLong() {
//            Collection = new List<long>();
//        }
//        public void GenerateCollection() {
//            Collection = new List<long>();
//            for (int i = 0; i < 1000; i++) {
//                Collection.Add(i);
//            }
//        }
//    }
//    [MemoryDiagnoser]
//    public class CollectionsComparer<T,TCollection> where T : IBench<TCollection>,new() {
//        private readonly IBench<TCollection> _benchCollection;
//        [GlobalSetup] 
//        public void Setup() {
//            IBench<TCollection> _benchCollection = new T();
//            //_benchCollection.GenerateCollection();
//        }
//        [Benchmark]
//        public void GenerateBench() => _benchCollection.GenerateCollection();
//    }
//    [MemoryDiagnoser]
//    public class CollectionsBench {
//        private readonly BenchListLong _benchCollection;
//        [GlobalSetup]
//        public void Setup() {
//            BenchListLong _benchCollection = new BenchListLong();
//            //_benchCollection.GenerateCollection();
//        }
//        [Benchmark]
//        public void GenerateBench() => _benchCollection.GenerateCollection();
//    }
//    public class Program {
//        static void Main(string[] args) {
//            BenchListLong list = new BenchListLong();
//            list.GenerateCollection();
//            //var summary = BenchmarkRunner.Run<CollectionsComparer<BenchListLong,List<long>>>();
//            var summary = BenchmarkRunner.Run<CollectionsBench>();
//        }
//    }
//}
