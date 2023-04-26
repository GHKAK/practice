using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

static void Main(string[] args) {
    var summary = BenchmarkRunner.Run<CollectionsComparer<ListBenchmark<int>, int>>();
}
public interface IBenchCollection<TCollection, TItem> where TCollection : ICollection<TItem>, new() {
    public TCollection GenerateCollection();
    public void AddBench();

}
public class ListBenchmark<T> : IBenchCollection<List<T>, T> {

}
public class CollectionsComparer<TCollection, TItem> where TCollection : IBenchCollection<TCollection, TItem>, ICollection<TItem>, new() {
    private TCollection _benchCollection;

    public CollectionsComparer() {
        _benchCollection = new TCollection();
        _benchCollection = _benchCollection.GenerateCollection();
    }
    [Benchmark]
    public void AddBench() => _benchCollection.AddBench();
}
