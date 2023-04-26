//using BenchmarkDotNet.Attributes;
//using BenchmarkDotNet.Running;

//static void Main(string[] args) {
//    var summary = BenchmarkRunner.Run<CollectionsComparer<ListBenchmark<int>,int>>();
//}
//public interface IBenchCollection<out TCollection, TItem> where TCollection : new() {
//    public TCollection GenerateCollection();
//    public void AddBench();

//}
////public class ListBenchmark<T> : IBenchCollection<List<T>, T> where T:IList<T>{
////    public List<T> GenerateCollection() { 
////        return new List<T>();
////    }
////    public void AddBench() { 
        
////    }
////}
//public class ListBenchmark<T> : List<T>, IBenchCollection<List<T>,T>{ 
    
//}
//public class CollectionsComparer<TCollection,TItem> where TCollection: ICollection<TItem>,IBenchCollection<TCollection, TItem>, new() {
//    private TCollection _benchCollection;
//    public CollectionsComparer() {
//        _benchCollection = new TCollection();
//        _benchCollection = _benchCollection.GenerateCollection();
//    }
//    public void AddBench() => _benchCollection.AddBench();
//}
